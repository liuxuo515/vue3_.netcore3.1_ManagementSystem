(function ($) {
    $.Organization = function () {
        var xhr; //这个参数是用于获取ajax请求，用户只存在一个ajax请求；
        function getIsDomestic() {
            var num = 0;

            function jointStr(list, obj1, obj2) {
                var o1 = "";
                var o2 = "";
                o2 += '<ul id="issues" class="issues">';
                for (var i = 0; i < list.length; i++) {
                    //                    if (list[i].OfficeName != '中国香港') {
                    o1 += '<li><a>' + list[i].OfficeName + '</a></li>';
                    o2 += '<li id="' + list[i].Identifier + '">';
                    o2 += '<div class="iss_con">';
                    o2 += '<h1>' + list[i].OfficeName + '</h1>';
                    o2 += '<div class="iss_p">';
                    o2 += '<a href="' + list[i].UrlName + '" target="_blank">了解更多>></a>';
                    if (list[i].Tel.length > 0) {
                        o2 += '<p>电&nbsp;&nbsp;话：<span>' + list[i].Tel + '</span></p>';
                    }
                    if (list[i].Fax.length > 0) {
                        o2 += '<p>传&nbsp;&nbsp;真：<span>' + list[i].Fax + '</span></p>';
                    }
                    if (list[i].Address.length > 0) {
                        o2 += '<p>地&nbsp;&nbsp;址：<span>' + list[i].Address + '</span></p>';
                    }
                    if (list[i].Email.length > 0) {
                        o2 += '<p>邮&nbsp;&nbsp;箱：<span>' + list[i].Email + '</span></p>';
                    }
                    if (list[i].ZipCode.length > 0) {
                        o2 += '<p>邮&nbsp;&nbsp;编：<span>' + list[i].ZipCode + '</span></p>';
                    }
                    o2 += '</div></div></li>';
                    //                    }
                }
                o2 += '</ul>';
                o2 += ' <div id="prev" class="prev"></div>';
                o2 += ' <div id="next" class="next"></div>';
                if (obj1 != undefined) {
                    //要删掉
                    if (list.length && list[0].OfficeArea === '境内') {
                        //o1 += '<li><a>西安</a></li>';
                    } else {
                        o1 += '<li><a>柏林</a></li><li><a>布拉迪斯拉发</a></li><li><a>布拉格</a></li><li><a>苏黎世</a></li><li><a>哈萨克斯坦</a></li><li><a>吉尔吉斯斯坦</a></li><li><a>蒙古</a></li><li><a>巴黎</a></li><li><a>波尔多</a></li><li><a>乌兹别克斯坦</a></li>';
                    }

                    obj1.html(o1);
                    $("li", obj1).eq(0).addClass("jg_select");
                }
                if (obj2 != undefined) {
                    obj2.html(o2);
                }
            }

            function option(isD) {
                if (xhr != undefined) {
                    xhr.abort();
                }
                //加载loading
                $("#jg_list").html('');
                $("#timeline").html('<p style="width:100%;text-align:center; margin-top:30px; color:#888;">Loading...</p>');

                var officeOption = {
                    type: "post",
                    url: "/Ajax/Index/GetOrganizationList.ashx",
                    data: { _: Math.random(), t: 'get_office_list', isd: isD },
                    success: function (json) {
                        if (json != undefined) {
                            //var data = eval('(' + json + ')');
                            var data = eval(json);
                            jointStr(data, $("#jg_list"), $("#timeline"));
                            
                            $().timelinr(); //调用时间轴
                        }
                    }
                };
                xhr = $.ajax(officeOption);
            }

            option(true);
            var $cardAnimate = $(".card_animate");
            $cardAnimate.css("width", $(".card_select").width());
            $cardAnimate.css("left", $(".card_select").offset().left);

            function cardAnimate(obj) {
                $cardAnimate.stop().animate({ left: $(".card_select").offset().left, width: $(".card_select").width() + 'px' }, 300);
            }

            $(".time_line_card .card").click(function () {
                num = $(this).index();
                $(this).addClass("card_select").siblings().removeClass("card_select");
                 console.log(num,'numnum');
                if (num == 0) {
                    $(".time_line").show();
                    option(num == 0 ? true : false);
                } else if (num == 1) {
                //盈科郑黄（福田）联营律师事务所
                    $(".time_line").hide();
                    let o1 = '<ul><li style="width: auto;"><a>盈科郑黄（福田）联营律师事务所</a></li></ul>'
                    $("#jg_list").html(o1);
                    $("li", $("#jg_list")).eq(0).addClass("jg_select");
                } else if (num == 4)  {
                //盈科全球一小时法律服务生态圈合作伙伴
                    $(".time_line").hide();
                    let o1 = '<div class="cityList" style="width:100%"><ul >   <li><div class="en-city">YK LAW LLP (NEW YORK)</div><div>盈科美国（纽约）律师事务所</div> </li><li><div class="en-city">YK LAW LLP (CALIFORNIA)</div> <div>盈科美国（洛杉矶）律师事务所</div></li> <li><div class="en-city">YK LAW LLP (IRVINE)</div> <div>盈科美国（尔湾）律师事务所</div></li> <li><div class="en-city">YK LAW LLP (San Francisco)</div><div>盈科美国（旧金山）律师事务所</div> </li> <li><div class="en-city">YK LAW LLP (NEW JERSEY)</div>  <div>盈科美国（新泽西）律师事务所</div> </li><li><div class="en-city">YK LAW CORPORATION</div> <div>盈科加拿大（温哥华）律师事务所</div> </li><li> <div class="en-city">YINGKE TORONTO CONSULTING CORPORATION</div><div>盈科多伦多咨询公司</div> </li><li> <div class="en-city">YK LAW ABOGADOS S.A.</div><div>盈科阿根廷律师事务所</div> </li> <li> <div class="en-city">YK Law Rechtsanwaltsgesellschaft mbH</div> <div>盈科德国（杜塞尔多夫）律师事务所</div> </li> <li> <div class="en-city">YK LAW ABOGADOS SLP</div> <div>盈科西班牙（马德里）律师事务所</div></li> <li> <div class="en-city">YK LAW MELBOURNE PTY LTD</div><div>盈科（墨尔本）律师事务所</div> </li>  <li> <div class="en-city">CE legal</div> <div>CE律师事务所</div></li><li> <div class="en-city">Hui Doe & Sum Law Firm LLP</div><div>香港许杜岑律师事务所</div> </li><li><div class="en-city">FOXWOOD LLC</div><div>新加坡FOXWOOD律师事务所</div></li> <li><div class="en-city">Y KONG, WONG & PARTNERS</div><div>马来西亚黄，林与孔律师事务所</div></li> <li><div class="en-city">俄罗斯盈宇律师事务所</div></li><li  style="padding-right: 40px;box-sizing: border-box;"><div class="en-city">Yingke Várnai Zamostny (Várnai & Partners Law Firm)</div> </li><li><div class="en-city">ADVANT-Nctm</div> </li><li> <div class="en-city">Grimaldi Alliance</div> </li><li><div class="en-city">JLPW Advocates & Solicitors</div></li><li style="padding-right: 40px;box-sizing: border-box;"><div class="en-city">NEKTOROV,SAVELIEV& PARTNERS（NSP）Law Firm</div></li><li><div class="en-city">ECIJA Mexico Law Firm</div> </li> <li><div class="en-city">Fegamo & Vasaf</div> </li><li> <div class="en-city">Pacheco Neto Sanden Teisseire-Advogados（PNST） Law Firm</div> </li><li><div class="en-city">Memery Crystal LLP</div></li><li><div class="en-city">Machas & Partners Law Firm</div></li> <li><div class="en-city">Ludovit Pavela Law Firm</div> </li> <li><div class="en-city">Martin Winkler & PKW Law Firm</div> </li> <li><div class="en-city">Reber Law Firm</div> </li><li><div class="en-city">Simon Associes Law Firm</div></li> <li> <div class="en-city">Kopilovic & Kopilovic Law Firm</div> </li> <li><div class="en-city">Cektir & Basari Law Firm</div> </li><li><div class="en-city">Barlas Law Firm</div></li> <li><div class="en-city">Lipa Meir & Co.</div></li> <li><div class="en-city">Ortus Africa</div> </li> <li><div class="en-city">Unicase LLP</div> </li><li><div class="en-city">DFDL</div></li> <li><div class="en-city">Trinaya Legal Advocates & Solicitors</div></li> <li><div class="en-city">BLC Law Firm</div> </li><li><div class="en-city">Yingke Várnai Zamostny (Várnai & Partners Law Firm)</div> </li> <li><div class="en-city">Adarve Corporación Jurídica Law Firm</div> </li></ul></div>';
                    $("#jg_list").html(o1);
                    $("li", $("#jg_list")).eq(0).addClass("jg_select");
                }else if(num == 3){
                //盈科海外机构
                  $(".time_line").hide();
                    let o1 = '<div class="cityList" style="width:100%"> <ul ><li ><div class="en-city" >YINGKE LAW FIRM LTD</div><div>盈科英国伦敦律师事务所有限公司</div></li><li ><div  class="en-city" >"YINGKE LAW FIRM" MCHJ</div><div>盈科乌兹别克斯坦律师事务所有限责任公司</div></li><li ><div  class="en-city" >Beijing Yingke Kazakhstan Law Firm </div><div>盈科哈萨克斯坦律师事务所有限责任合伙企业</div></li><li ><div  class="en-city" >YINGKE CONSULTING SDN.BHD</div><div>盈科（马来西亚）咨询有限公司</div></li><li ><div class="en-city" >YINGKE INTERNATIONAL INDONESIA</div><div>盈科国际印度尼西亚有限公司</div></li><li ><div  class="en-city" >YINGKE Management Consultancy QFZ LLC</div><div>盈科卡塔尔管理咨询有限公司</div></li><li ><div class="en-city"  >YINGKE CONSULTING PHILIPPINES INC.</div><div>盈科菲律宾咨询公司</div></li><li ><div  class="en-city" >Business Profile (Company) of YINGKE CONSULTING(SINGAPORE) PTE. LTD.</div><div>盈科新加坡咨询有限公司</div></li><li style="padding-right: 20px;box-sizing: border-box;"><div  class="en-city" >Yingke Afghan Business\' Legal Consultancy Services Company</div><div>盈科阿富汗商务法律咨询服务公司</div></li><li ><div  class="en-city" >YINGKE DANISMANLIK LIMITED SIRKETI</div><div>盈科咨询有限公司</div></li><li ><div  class="en-city" >YINGKE NZ LIMITED</div><div>盈科新西兰有限公司</div></li><li ><div  class="en-city" >YINGKE S.R.L</div><div>盈科意大利（米兰）有限责任公司</div></li><li ><div  class="en-city" >YK LAW AVVOCATI S.R.L.</div><div>盈科（罗马）法律咨询有限公司</div></li><li ><div  class="en-city" >YK Law Tax Advisory Kft.</div><div>盈科（匈牙利）法律税务咨询有限公司</div></li><li style="padding-right: 20px;box-sizing: border-box;"><div  class="en-city" >Yingke consultancy limited</div><div>盈科非洲（埃及）咨询公司</div></li><li ><div  class="en-city" >YINGKE AFRICA ANGO-ONSULTORIA, LDA</div><div>盈科非洲（安哥拉）咨询公司</div></li><li style="padding-right: 20px;box-sizing: border-box;"><div  class="en-city" >YINGKE AFRICA CONSULTANCY LIMITED</div><div>盈科非洲（肯尼亚）咨询公司</div></li><li ><div  class="en-city" >YINGKE AFRICA SA CONSULTANCY(PTY) LTD</div><div>盈科非洲（南非）咨询公司</div></li><li ><div  class="en-city" >YINGKE AFICA(NIGERIA)CONSULTANCY LIMITED</div><div>盈科非洲（尼日利亚）咨询公司</div></li><li ><div  class="en-city" >YINGKE AFRICA TAN CONSULTANCY LIMITED</div><div>盈科非洲（坦桑尼亚）咨询公司</div></li><li ><div  class="en-city" >YINGKE CONSULTING SERVICE PLC</div><div>盈科非洲（埃塞俄比亚）咨询公司</div></li><li style="padding-right: 20px;box-sizing: border-box;"><div  class="en-city" >YINGKE INTERNATIONAL INVESTMENTS (PRIVATE) LIMITED</div><div>盈科非洲（津巴布韦）国际投资有限公司</div></li><li ><div  class="en-city" >YINGKE PORTUGAL, UNIPESSOAL LDA</div><div>盈科葡萄牙咨询</div></li><li ><div  class="en-city" >China Yingke Legal Consulting Co. Ltd</div><div>中国盈科法律咨询有限责任公司</div></li><li ><div  class="en-city" >YINGKE FRANCE</div><div>盈科法国有限责任公司</div></li><li ><div  class="en-city" >Yingke Law srl</div><div>盈科法律咨询有限公司</div></li><li ><div  class="en-city" >YINGKE (CAMBODIA) CONSULTING CO. LTD</div><div>盈科（柬埔寨）律师事务所</div></li></ul></div>';
                    $("#jg_list").html(o1);
                    $("li", $("#jg_list")).eq(0).addClass("jg_select");
                } else{
                //num == 2 盈科海外律师事务所
                 $(".time_line").hide();
                        let o1 = '<div class="cityList" style="width:100%"> <ul > <li ><div >盈科外国法事务律师事务所（日本）</div></li><li ><div >盈科外国法咨询律师事务所（韩国）</div></li><li ><div class="en-city" >YINGKE LAW FIRM</div><div >北京盈科律师事务所河内分所（越南）</div></li><li ><div >盈科缅甸律师事务所有限责任公司</div></li><li ><div >盈科老挝律师事务所</div></li><li ><div >盈科（泰国）律师事务所</div></li><li ><div >盈科澳大利亚(悉尼)有限公司</div></li><li ><div >盈科慕尼黑律师事务所有限公司</div></li></ul></div>';
                    $("#jg_list").html(o1);
                    $("li", $("#jg_list")).eq(0).addClass("jg_select");
                }

                cardAnimate(this);
            }).mousemove(function () {
                $cardAnimate.stop().animate({ left: $(this).offset().left, width: $(this).width() + 'px' }, 300);
            }).mouseleave(function () {
                cardAnimate(this);
            });
            $(window).resize(function () {
                $cardAnimate.css("width", $(".card_select").width());
                $cardAnimate.css("left", $(".card_select").offset().left);
            });
            //国内外切换动画
            //加载机构列表
            //创建html
        }
        getIsDomestic();
    }

}(jQuery));