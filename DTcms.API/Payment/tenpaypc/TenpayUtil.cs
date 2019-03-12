using System;
using System.Xml;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.API.Payment.tenpaypc
{
	/// <summary>
	/// TenpayUtil ��ժҪ˵����
	/// </summary>
	public class TenpayUtil
	{
        public string tenpay = "1";
        public string partner = ""; //�Ƹ�ͨ�̻���
        public string key  = ""; //�Ƹ�ͨ��Կ;
        public string return_url = ""; //��ʾ֧��֪ͨҳ��;
        public string notify_url = ""; //֧����ɺ�Ļص�����ҳ��;

        public TenpayUtil(int site_payment_id)
        {
            Model.site_payment model = new BLL.site_payment().GetModel(site_payment_id); //վ��֧����ʽ
            if (model != null)
            {
                Model.payment payModel = new BLL.payment().GetModel(model.payment_id); //֧��ƽ̨
                Model.sites siteModel = new BLL.sites().GetModel(model.site_id); //վ������
                Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //ϵͳ����

                partner = model.key1; //�̻��ţ��������ã�
                key = model.key2; //�̻�֧����Կ���ο������ʼ����ã��������ã�
                //�ص������ַ
                if (!string.IsNullOrEmpty(siteModel.domain.Trim()) && siteModel.is_default == 0) //������Զ��������Ҳ���Ĭ��վ��
                {
                    return_url = "http://" + siteModel.domain + payModel.return_url;
                    notify_url = "http://" + siteModel.domain + payModel.notify_url;
                }
                else if (siteModel.is_default == 0) //����Ĭ��վ��Ҳû�а�����
                {
                    return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + siteModel.build_path.ToLower() + payModel.return_url;
                    notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + siteModel.build_path.ToLower() + payModel.notify_url;
                }
                else //����ʹ�õ�ǰ����
                {
                    return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + payModel.return_url;
                    notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + payModel.notify_url;
                }
            }
        }

		/** ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ�� */
		public UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** ȡ����� */
		public string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
	}
}