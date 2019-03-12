﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.article" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2017/9/21 3:52:03.
		本页面代码由DTcms模板引擎生成于 2017/9/21 3:52:03. 
	*/

	base.channel = "news";
	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>新闻资讯 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/jquery.flexslider-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n    $(\"#focus-box\").flexslider({\r\n        directionNav: false,\r\n		pauseOnAction: false\r\n	});\r\n});\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body id=\"news\">\r\n<!--页面头部-->\r\n");

	templateBuilder.Append("<div class=\"header\">\r\n    <div class=\"head-top\">\r\n        <div class=\"section\">\r\n            <div class=\"left-box\">\r\n                <span>网站链接：</span>\r\n                <a target=\"_blank\" href=\"http://www.dtcms.net\">动力启航官网</a>\r\n                <a target=\"_blank\" href=\"http://demo.dtcms.net\">DTcms演示站</a>\r\n            </div>\r\n            <script type=\"text/javascript\">\r\n                $.ajax({\r\n                    type: \"POST\",\r\n                    url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_check_login\",\r\n                    dataType: \"json\",\r\n                    timeout: 20000,\r\n                    success: function (data, textStatus) {\r\n                        if (data.status == 1) {\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a><strong>|</strong>');\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a>');\r\n                        } else {\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">注册</a><strong>|</strong>');\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a>');\r\n                        }\r\n                    }\r\n                });\r\n            </");
	templateBuilder.Append("script>\r\n            <div id=\"menu\" class=\"right-box\">\r\n                <a href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\"><i class=\"iconfont icon-phone\"></i>联系我们</a>\r\n                <a href=\"");
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\"><i class=\"iconfont icon-cart\"></i>购物车(<span id=\"shoppingCartCount\"><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_cart_count\"></");
	templateBuilder.Append("script></span>)</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"head-nav\">\r\n        <div class=\"section\">\r\n            <div class=\"logo\">\r\n                <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/logo.png\" /></a>\r\n            </div>\r\n            <div class=\"nav-box\">\r\n                <ul>\r\n                    <li class=\"index\"><a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a></li>\r\n                    <li class=\"news\"><a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a></li>\r\n                    <li class=\"goods\"><a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a></li>\r\n                    <li class=\"video\"><a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频中心</a></li>\r\n                    <li class=\"photo\"><a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片分享</a></li>\r\n                    <li class=\"down\"><a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载</a></li>\r\n                </ul>\r\n            </div>\r\n            <div class=\"search-box\">\r\n                <div class=\"input-box\">\r\n                    <input id=\"keywords\" name=\"keywords\" type=\"text\" onkeydown=\"if(event.keyCode==13){SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');return false};\" placeholder=\"输入关健字\" x-webkit-speech=\"\" />\r\n                </div>\r\n                <a href=\"javascript:;\" onclick=\"SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');\">\r\n                    <i class=\"iconfont icon-search\"></i>\r\n                </a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/页面头部-->\r\n\r\n<!--当前位置-->\r\n<div class=\"section\">\r\n    <div class=\"location\">\r\n        <span>当前位置：</span>\r\n        <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<!--推荐资讯-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <div class=\"left-455\" style=\"margin:0;height:341px;\">\r\n                <div id=\"focus-box\" class=\"focus-box\">\r\n                    <ul class=\"slides\">\r\n                    ");
	DataTable focusNews = get_article_list(channel, 0, 8, "status=0 and is_slide=1 and img_url<>''");

	foreach(DataRow dr in focusNews.Rows)
	{

	templateBuilder.Append("\r\n                        <li>\r\n                            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                                <span class=\"note-bg\"></span>\r\n                                <span class=\"note-txt\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n                                <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                            </a>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <div class=\"left-455\">\r\n                <ul class=\"side-txt-list\">\r\n                ");
	DataTable newsList = get_article_list(channel, 0, 10, "status=0");

	int newdr__loop__id=0;
	foreach(DataRow newdr in newsList.Rows)
	{
		newdr__loop__id++;


	if (newdr__loop__id==1||newdr__loop__id==6)
	{

	templateBuilder.Append("\r\n                    <li class=\"tit\"><a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newdr["title"]) + "</a></li>\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <li><span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(newdr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span><a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newdr["title"]) + "</a></li>\r\n                    ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            <div class=\"left-220\">\r\n                <ul class=\"side-img-list\">\r\n                ");
	DataTable topNewsList = get_article_list(channel, 0, 4, "status=0 and is_top=1 and img_url<>''");

	int topdr__loop__id=0;
	foreach(DataRow topdr in topNewsList.Rows)
	{
		topdr__loop__id++;


	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"img-box\">\r\n                            <label>");
	templateBuilder.Append(Utils.ObjectToStr(topdr__loop__id));
	templateBuilder.Append("</label>\r\n                            <img src=\"" + Utils.ObjectToStr(topdr["img_url"]) + "\" />\r\n                        </div>\r\n                        <div class=\"txt-box\">\r\n                            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(topdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(topdr["title"]) + "</a>\r\n                            <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(topdr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/推荐资讯-->\r\n\r\n<!--分类推荐-->\r\n<div class=\"section\">\r\n    ");
	DataTable categoryList = get_category_child_list(channel,0);

	foreach(DataRow cdr in categoryList.Rows)
	{

	templateBuilder.Append("\r\n    <!--子类-->\r\n    <div class=\"main-tit\">\r\n        <h2>" + Utils.ObjectToStr(cdr["title"]) + "</h2>\r\n        <p>\r\n            ");
	DataTable bcategoryList = get_category_child_list(channel,Utils.StrToInt(Utils.ObjectToStr(cdr["id"]), 0));

	foreach(DataRow dr in bcategoryList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news_list",Utils.ObjectToStr(cdr["id"])));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <!--/子类-->\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <!--左边-->\r\n            <div class=\"left-925\">\r\n                <div class=\"pad-15 bg-wrap\">\r\n                    <ul class=\"rel-list\">\r\n                    ");
	DataTable dt = get_article_list(channel, Utils.StrToInt(Utils.ObjectToStr(cdr["id"]), 0), 6, "status=0 and img_url<>''");

	foreach(DataRow dr1 in dt.Rows)
	{

	templateBuilder.Append("\r\n                        <li>\r\n                            <div class=\"img-box\">\r\n                                <a title=\"" + Utils.ObjectToStr(dr1["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr1["id"])));

	templateBuilder.Append("\">\r\n                                    <img src=\"" + Utils.ObjectToStr(dr1["img_url"]) + "\">\r\n                                </a>\r\n                            </div>\r\n                            <div class=\"info\">\r\n                                <h3><a title=\"" + Utils.ObjectToStr(dr1["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr1["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr1["title"]) + "</a></h3>\r\n                                <p>" + Utils.ObjectToStr(dr1["zhaiyao"]) + "</p>\r\n                                <span>" + Utils.ObjectToStr(dr1["add_time"]) + "</span>\r\n                            </div>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <!--/左边-->\r\n            \r\n            <!--右边-->\r\n            <div class=\"left-220\">\r\n                <div class=\"pad-15 bg-wrap\">\r\n                    <ul class=\"focus-list\">\r\n                    ");
	DataTable hotList = get_article_list(channel, Utils.StrToInt(Utils.ObjectToStr(cdr["id"]), 0), 10, "status=0", "click desc,id desc");

	int dr2__loop__id=0;
	foreach(DataRow dr2 in hotList.Rows)
	{
		dr2__loop__id++;


	templateBuilder.Append("\r\n                        <li>\r\n                            ");
	if (dr2__loop__id<2)
	{

	templateBuilder.Append("\r\n                            <label class=\"hot\">");
	templateBuilder.Append(Utils.ObjectToStr(dr2__loop__id));
	templateBuilder.Append("</label>\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                            <label>");
	templateBuilder.Append(Utils.ObjectToStr(dr2__loop__id));
	templateBuilder.Append("</label>\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr2["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr2["title"]) + "</a>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <!--/右边-->\r\n        </div>\r\n    </div>\r\n    ");
	}	//end for if

	templateBuilder.Append("\r\n</div>\r\n<!--/分类推荐-->\r\n\r\n<!--页面底部-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n    <div class=\"section\">\r\n        <div class=\"foot-nav\">\r\n            <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频专区</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">图片分享</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">资源下载</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">在线留言</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a>\r\n        </div>\r\n        <div class=\"foot-box\">\r\n            <div class=\"copyright\">\r\n                <p>版权所有 ");
	templateBuilder.Append(Utils.ObjectToStr(site.company));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(site.crod));
	templateBuilder.Append("</p>\r\n                <p>公司地址：");
	templateBuilder.Append(Utils.ObjectToStr(site.address));
	templateBuilder.Append(" 联系电话：");
	templateBuilder.Append(Utils.ObjectToStr(site.tel));
	templateBuilder.Append("</p>\r\n                <p class=\"gray\">Copyright © 2009-2017 dtcms.net Corporation,All Rights Reserved.</p>\r\n            </div>\r\n            <div class=\"service\">\r\n                <p>周一至周日 9:00-24:00</p>\r\n                <a href=\"http://www.dtcms.net\" target=\"_blank\"><i class=\"iconfont icon-phone\"></i>在线客服</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/页面底部-->\r\n</body>\r\n</html>");
	Response.Write(templateBuilder.ToString());
}
</script>
