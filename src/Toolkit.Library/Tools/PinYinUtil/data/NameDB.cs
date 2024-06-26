﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 使用姓名数据库查询
    /// </summary>
    internal class NameDB
    {
        // 实例
        private static NameDB instance;

        private readonly Dictionary<string, string> map;

        /// <summary>
        ///  获取单实例
        /// </summary>
        public static NameDB Instance
        {
            get { return instance ?? (instance = new NameDB()); }
        }

        /// <summary>
        /// 私有构造
        /// </summary>
        private NameDB()
        {
            map = new Dictionary<string, string>();

            LoadResource();
        }

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="data"></param>
        /// <param name="replace"></param>
        public void Update(Dictionary<string, string> data, bool replace = false)
        {
            foreach (var item in data)
            {
                if (!map.ContainsKey(item.Key))
                {
                    map.Add(item.Key, item.Value);
                    continue;
                }
                if (!replace)
                {
                    continue;
                }
                map[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// 加载拼音库资源
        /// </summary>
        public void LoadResource()
        {
            foreach (var row in DATA.Split('\n'))
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }
                var temp = row.Split('=');
                // 取姓
                var name = temp[0];

                // 取拼音串 小心有个 \r 的回车符号
                var pinyin = temp[1].Trim();

                map.Add(name, pinyin.Replace(",", " "));
            }
        }

        /// <summary>
        /// 获取汉字的拼音
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns>若未找到汉字拼音，则返回空</returns>
        public string GetPinyin(string hanzi)
        {
            return map.ContainsKey(hanzi) ? map[hanzi] : null;
        }

        /// <summary>
        /// 根据拼音获取汉字
        /// </summary>
        /// <param name="pinyin">拼音</param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符，此参数用于告知传入的拼音是完整拼音还是仅仅是声母</param>
        /// <returns></returns>
        public IEnumerable<string> GetHanzi(string pinyin, bool matchAll)
        {
            var reg = new Regex("[0-9]");
            // 完全匹配
            if (matchAll)
            {
                // 查询到匹配的拼音的汉字
                return map.Where(item => reg.Replace(item.Value, "").Equals(pinyin))
                    .Select(item => item.Key).ToArray();
            }
            // 匹配开头部分
            else
            {
                // 查询到匹配的拼音的unicode编码
                return map.Where(item => reg.Replace(item.Value, "").StartsWith(pinyin))
                    .Select(item => item.Key).ToArray();
            }
        }

        private const string DATA = @"艾=ai4
安=an1
敖=ao2
巴=ba1
白=bai2
柏=bai3
班=ban1
包=bao1
暴=bao4
鲍=bao4
贝=bei4
贲=ben1
毕=bi4
边=bian1
卞=bian4
别=bie2
邴=bing3
薄=bo2
卜=bu3
步=bu4
蔡=cai4
苍=cang1
曹=cao2
岑=cen2
柴=chai2
单于=chan2,yu2
昌=chang1
常=chang2
巢=chao2
晁=chao2
车=che1
陈=chen2
成=cheng2
程=cheng2
池=chi2
充=chong1
储=chu3
褚=chu3
淳于=chun2,yu2
从=cong2
崔=cui1
戴=dai4
党=dang3
邓=deng4
狄=di2
第五=di4,wu3
刁=diao1
丁=ding1
东方=dong1,fang1
东=dong1
董=dong3
窦=dou4
都=du1
堵=du3
杜=du4
段=duan4
鄂=e4
樊=fan2
范=fan4
方=fang1
房=fang2
费=fei4
丰=feng1
封=feng1
酆=feng1
冯=feng2
凤=feng4
伏=fu2
扶=fu2
福=fu2
符=fu2
傅=fu4
富=fu4
干=gan1
甘=gan1
高=gao1
郜=gao4
戈=ge1
盖=ge3
葛=ge3
耿=geng3
公孙=gong1,sun1
公羊=gong1,yang2
公冶=gong1,ye3
宗政=gong1,ye3
公=gong1
宫=gong1
弓=gong1
龚=gong1
巩=gong3
贡=gong4
勾=gou1
古=gu3
谷=gu3
顾=gu4
关=guan1
管=guan3
广=guang3
桂=gui4
郭=guo1
国=guo2
韩=han2
杭=hang2
郝=hao3
何=he2
和=he2
赫连=he4,lian2
贺=he4
衡=heng2
弘=hong2
洪=hong2
红=hong2
侯=hou2
後=hou4
胡=hu2
扈=hu4
花=hua1
滑=hua2
华=hua4
怀=huai2
桓=huan2
宦=huan4
皇甫=huang2,fu3
黄=huang2
惠=hui4
霍=huo4
姬=ji1
嵇=ji1
吉=ji2
汲=ji2
纪=ji3
冀=ji4
季=ji4
暨=ji4
蓟=ji4
计=ji4
家=jia1
郏=jia2
贾=jia3
简=jian3
姜=jiang1
江=jiang1
蒋=jiang3
焦=jiao1
金=jin1
靳=jin4
经=jing1
荆=jing1
井=jing3
景=jing3
居=ju1
鞠=ju1
阚=kan4
康=kang1
柯=ke1
空=kong1
孔=kong3
寇=kou4
蒯=kuai3
匡=kuang1
夔=kui2
隗=kui2
赖=lai4
蓝=lan2
郎=lang2
劳=lao2
雷=lei2
冷=leng3
黎=li2
李=li3
利=li4
厉=li4
郦=li4
廉=lian2
连=lian2
梁=liang2
廖=liao4
林=lin2
蔺=lin4
令狐=ling2,hu2
凌=ling2
刘=liu2
柳=liu3
隆=long2
龙=long2
娄=lou2
闾丘=lu2,qiu1
卢=lu2
吕=lu3
鲁=lu3
禄=lu4
路=lu4
逯=lu4
陆=lu4
栾=luan2
罗=luo2
骆=luo4
麻=ma2
马=ma3
满=man3
毛=mao2
茅=mao2
梅=mei2
蒙=meng2
孟=meng4
糜=mi2
米=mi3
宓=mi4
苗=miao2
缪=miao4
闵=min3
明=ming2
万俟=mo4,qi2
莫=mo4
慕容=mu4,rong2
慕=mu4
牧=mu4
穆=mu4
那=na1
能=nai4
倪=ni2
乜=nie4
聂=nie4
宁=ning4
牛=niu2
钮=niu3
农=nong2
欧阳=ou1,yang2
欧=ou1
潘=pan1
庞=pang2
逄=pang2
裴=pei2
彭=peng2
蓬=peng2
皮=pi2
平=ping2
濮阳=pu2,yang2
濮=pu2
蒲=pu2
浦=pu3
戚=qi1
祁=qi2
齐=qi2
钱=qian2
强=qiang2
乔=qiao2
秦=qin2
秋=qiu1
邱=qiu1
仇=qiu2
裘=qiu2
屈=qu1
麴=qu1
璩=qu2
瞿=qu2
全=quan2
权=quan2
阙=que1
冉=ran3
饶=rao2
任=ren2
容=rong2
戎=rong2
荣=rong2
融=rong2
茹=ru2
阮=ruan3
芮=rui4
桑=sang1
沙=sha1
山=shan1
单=shan4
上官=shang4,guan1
尚=shang4
韶=shao2
邵=shao4
厍=she4
申屠=shen1,tu2
申=shen1
莘=shen1
沈=shen3
慎=shen4
盛=sheng
师=shi1
施=shi1
时=shi2
石=shi2
史=shi3
寿=shou4
殳=shu1
舒=shu1
束=shu4
双=shuang1
水=shui3
司空=si1,kong1
司马=si1,ma3
司徒=si1,tu2
司=si1
松=song1
宋=song4
苏=su1
宿=su4
孙=sun1
索=suo3
邰=tai2
太叔=tai4,shu1
澹台=tan2,tai2
谈=tan2
谭=tan2
汤=tang1
唐=tang2
陶=tao2
滕=teng2
田=tian2
通=tong1
佟=tong2
童=tong2
钭=tou3
屠=tu2
万=wan4
汪=wang1
王=wang2
危=wei1
韦=wei2
卫=wei4
蔚=wei4
魏=wei4
温=wen1
闻人=wen2,ren2
文=wen2
闻=wen2
翁=weng1
沃=wo4
乌=wu1
巫=wu1
邬=wu1
吴=wu2
毋=wu2
伍=wu3
武=wu3
奚=xi1
郗=xi1
习=xi2
席=xi2
郤=xi4
夏侯=xia4,hou2
夏=xia4
鲜于=xian1,yu2
咸=xian2
向=xiang4
相=xiang4
项=xiang4
萧=xiao1
解=xie4
谢=xie4
辛=xin1
邢=xing2
幸=xing4
熊=xiong2
胥=xu1
须=xu1
徐=xu2
许=xu3
轩辕=xuan1,yuan2
宣=xuan1
薛=xue1
荀=xun2
燕=yan1
严=yan2
言=yan2
阎=yan2
颜=yan2
晏=yan4
杨=yang2
羊=yang2
仰=yang3
养=yang3
姚=yao2
叶=ye4
伊=yi1
易=yi4
益=yi4
羿=yi4
殷=yin1
阴=yin1
尹=yin3
印=yin4
应=ying1
雍=yong1
尤=you2
游=you2
於=yu1
于=yu2
余=yu2
俞=yu2
虞=yu2
鱼=yu2
宇文=yu3,wen2
庾=yu3
禹=yu3
尉迟=yu4,chi2
喻=yu4
郁=yu4
元=yuan2
袁=yuan2
乐=yue4
越=yue4
云=yun2
宰=zai3
昝=zan3
臧=zang1
曾=zeng1
查=zha1
翟=zhai2
詹=zhan1
湛=zhan4
张=zhang1
章=zhang1
长孙=zhang3,sun1
赵=zhao4
甄=zhen1
郑=zheng4
支=zhi1
钟离=zhong1,li2
仲孙=zhong1,sun1
终=zhong1
钟=zhong1
仲=zhong4
周=zhou1
诸葛=zhu1,ge3
朱=zhu1
诸=zhu1
竺=zhu2
祝=zhu4
庄=zhuang1
卓=zhuo2
訾=zi3
宗=zong1
邹=zou1
祖=zu3
左=zuo3";
    }
}