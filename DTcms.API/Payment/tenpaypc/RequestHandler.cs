using System;
using System.Collections;
using System.Text;
using System.Web;

namespace DTcms.API.Payment.tenpaypc
{
	/// <summary>
	/// RequestHandler ��ժҪ˵����
	/// </summary>
	public class RequestHandler
	{
		public RequestHandler(HttpContext httpContext)
		{
			parameters = new Hashtable();

			this.httpContext = httpContext;
			this.setGateUrl("https://www.tenpay.com/cgi-bin/v1.0/service_gate.cgi");
		}

		/** ����url��ַ */
		private string gateUrl;
		
		/** ��Կ */
		private string key;
		
		/** ����Ĳ��� */
		protected Hashtable parameters;
		
		/** debug��Ϣ */
		private string debugInfo;

		protected HttpContext httpContext;
		
		/** ��ʼ��������*/
		public virtual void init() 
		{
			//nothing to do
		}

		/** ��ȡ��ڵ�ַ,����������ֵ */
		public String getGateUrl() 
		{
			return gateUrl;
		}

		/** ������ڵ�ַ,����������ֵ */
		public void setGateUrl(String gateUrl) 
		{
			this.gateUrl = gateUrl;
		}

		/** ��ȡ��Կ */
		public String getKey() 
		{
			return key;
		}

		/** ������Կ */
		public void setKey(string key) 
		{
			this.key = key;
		}

		/** ��ȡ������������URL  @return String */
		public virtual string getRequestURL()
		{
			this.createSign();

			StringBuilder sb = new StringBuilder();
			ArrayList akeys=new ArrayList(parameters.Keys); 
			akeys.Sort();
			foreach(string k in akeys)
			{
				string v = (string)parameters[k];
				if(null != v && "key".CompareTo(k) != 0) 
				{
                    sb.Append(k + "=" + MD5Util.UrlEncode(v, getCharset()) + "&");
				}
			}

			//ȥ�����һ��&
			if(sb.Length > 0)
			{
				sb.Remove(sb.Length-1, 1);
			}
							
			return this.getGateUrl() + "?" + sb.ToString();
		}

		/**
		* ����md5ժҪ,������:����������a-z����,������ֵ�Ĳ������μ�ǩ����
		*/
		protected virtual void createSign() 
		{
			StringBuilder sb = new StringBuilder();

			ArrayList akeys=new ArrayList(parameters.Keys); 
			akeys.Sort();

			foreach(string k in akeys)
			{
				string v = (string)parameters[k];
				if(null != v && "".CompareTo(v) != 0
					&& "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0) 
				{
					sb.Append(k + "=" + v + "&");
				}
			}

			sb.Append("key=" + this.getKey());
			string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();
		
			this.setParameter("sign", sign);
		
			//debug��Ϣ
			this.setDebugInfo(sb.ToString() + " => sign:" + sign);		
		}

		/** ��ȡ����ֵ */
		public string getParameter(string parameter) 
		{
			string s = (string)parameters[parameter];
			return (null == s) ? "" : s;
		}

		/** ���ò���ֵ */
		public void setParameter(string parameter,string parameterValue) 
		{
			if(parameter != null && parameter != "")
			{
				if(parameters.Contains(parameter))
				{
					parameters.Remove(parameter);
				}
	
				parameters.Add(parameter,parameterValue);		
			}
		}

		public void doSend()
		{
			this.httpContext.Response.Redirect(this.getRequestURL());
		}
			
		/** ��ȡdebug��Ϣ */
		public String getDebugInfo() 
		{
			return debugInfo;
		}

		/** ����debug��Ϣ */
		public void setDebugInfo(String debugInfo) 
		{
			this.debugInfo = debugInfo;
		}

		public Hashtable getAllParameters()
		{
			return this.parameters;
		}

		protected virtual string getCharset()
		{
			return this.httpContext.Request.ContentEncoding.BodyName;
		}
	}
}
