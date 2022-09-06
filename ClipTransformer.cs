﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

//建立元件转换类
class ClipTransformer
{
    //创建cr实例
    ClipReader cr = new ClipReader();
    //创建ta实例
    TransformAction ta = new TransformAction();
    //预置ca数组以记录所有元件名称（包括被引用的）
    public ArrayList ca = new ArrayList();
    //预置cca数组以记录所有元件名称（包括被引用的）
    public ArrayList cca = new ArrayList();
    //预置元件类型表示
    public int icca = 0;
    //预置about
    public string about = null;
    //预置json名称及属性
    public string oname, jname, j1, j2, j3, j4, j5, o1, o2, o3, o4, o5;

    //生成序号重写选择部分
    public int NumRewrite()
    {
        int num = 0;
        Console.WriteLine("是否进行元件序号重写？重写输入1或者y，不重写输入0或n（不输入按回车默认重写）");
        //选项收录
        string s = Console.ReadLine();
        if (s == "" || s == "/n/n" || s == "1" || s == "y")
        {
            num = 1;
        }
        else if(s == "0" || s == "n")
        {
            num = 0;
        }
        else
        {
            Console.WriteLine("输入错误，执行默认重选操作");
            num=NumRewrite();
        }
            return num;
    }
    //生成选择部分
    public void Select(ArrayList cca)
    {
        try
        {
            //预定义元件前缀
            string aa = null, ii = null, ma = null;
            Console.WriteLine("请根据以下需求输入相应序号并按回车键（不输入按回车默认转太極的xfl）\n1.转SPC-Util的xfl\n2.转太極的xfl\n3.转TwinKles-ToolKit的xfl（因数据结构问题停止支持）\n4.转PopStudio的xfl（因数据结构问题停止支持）\n5.转SPCUtil的xfl（Android）\n6.转太極的xfl（Android）\n注：请务必按照说明书严格执行规范操作！！！");
            //选项收录
            string s = Console.ReadLine();
            //判定是否直接回车，直接回车则执行2，1为转SPC-Util的xfl，2为转太極的xfl，3为转TwinKles-ToolKit的xfl，4为转PopStudio的xfl，5为转SPCUtil的xfl（Android），6为转太極的xfl（Android）
            if (s == "1")
            {
                //前缀赋值
                aa = "A_";
                ii = "M_";
                ma = "A_Main";
                about = "This XFL is convert from PAM file, By SPC-Util.";
                jname = "OtherInfo.json";
                j1 = "UnK";
                j2 = "Origin";
                j3 = "ImageSize";
                j4 = "ImageMapper";
                j5 = "SubAnimMapper";
                //转换数组
                ArrayListTransform(cca, aa, ii, ma);
            }
            else if (s == "" || s == "/n/n" || s == "2")
            {
                //前缀赋值
                aa = "a";
                ii = "i";
                ma = "main";
                about = "this XFL is convert from Popcap-AniMation file , by TaiJi .";
                jname = "extra.json";
                j1 = "unk";
                j2 = "origin";
                j3 = "imgSz";
                j4 = "imgMapper";
                j5 = "animMapper";
                //转换数组
                ArrayListTransform(cca, aa, ii, ma);
            }
            else if (s == "3")
            {
                //前缀赋值
                aa = "animation_";
                ii = "sprite_";
                ma = "main_animation";
                jname = "extra.json";
                j1 = "unknown";
                j2 = "origin";
                j3 = "imgSz";
                j4 = "imgMapper";
                j5 = "animMapper";
                //停止支持//转换数组
                //停止支持///ArrayListTransform(cca, aa, ii, ma);
                Console.WriteLine("功能因数据结构停止支持，执行默认重选操作");
                Select(cca);
            }
            else if (s == "4")
            {
                //前缀赋值
                aa = "animation_";
                ii = "sprite_";
                ma = "main_animation";
                jname = "extra.json";
                j1 = "unknown";
                j2 = "origin";
                j3 = "imgSz";
                j4 = "imgMapper";
                j5 = "animMapper";
                //停止支持//转换数组
                //停止支持///ArrayListTransform(cca, aa, ii, ma);
                Console.WriteLine("功能因数据结构停止支持，执行默认重选操作");
                Select(cca);
            }
            else if (s == "5")
            {
                //前缀赋值
                aa = "A_";
                ii = "M_";
                ma = "A_Main";
                about = "This XFL is convert from PAM file, By SPC-Util.";
                jname = "OtherInfo.json";
                j1 = "UnK";
                j2 = "Origin";
                j3 = "ImageSize";
                j4 = "ImageMapper";
                j5 = "SubAnimMapper";
                //转换数组
                ArrayListTransform(cca, aa, ii, ma);
            }
            else if (s == "6")
            {
                //前缀赋值
                aa = "a";
                ii = "i";
                ma = "main";
                about = "this XFL is convert from Popcap-AniMation file , by TaiJi .";
                jname = "extra.json";
                j1 = "unk";
                j2 = "origin";
                j3 = "imgSz";
                j4 = "imgMapper";
                j5 = "animMapper";
                //转换数组
                ArrayListTransform(cca, aa, ii, ma);
            }
            else
            {
                Console.WriteLine("输入数字错误，执行默认重选操作");
                Select(cca);
            }
        }
        catch
        {
            Console.WriteLine("Select ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }

    //生成数组转换部分
    public void ArrayListTransform(ArrayList cca,string aa,string ii,string ma)
    {
        try
        {
            //判断是否重写元件序号
            int num = NumRewrite(), aj = 0, ij = 0;
            if (num == 1)
            {
                Console.WriteLine("元件序号重写中......");
            }
            else { }
            if (cca.Contains("A_Main"))
            {
                icca = 1;
                oname = "OtherInfo.json";
                o1 = "UnK";
                o2 = "Origin";
                o3 = "ImageSize";
                o4 = "ImageMapper";
                o5 = "SubAnimMapper";
            }
            else if (cca.Contains("main"))
            {
                icca = 2;
                oname = "extra.json";
                o1 = "unk";
                o2 = "origin";
                o3 = "imgSz";
                o4 = "imgMapper";
                o5 = "animMapper";
            }
            else if (cca.Contains("main_animation"))
            {
                icca = 0;
                oname = "extra.json";
                o1 = "unknown";
                o2 = "origin";
                o3 = j3;
                o4 = j4;
                o5 = j5;
                Console.WriteLine("数据结构不支持，不予转换");
            }
            else
            {
                Console.WriteLine("缺少核心元件或数据结构不支持，不予转换");
            }
            for (int i = 0; i < cca.Count; i++)
            {
                if (icca == 1)
                {
                    cca[i] = cca[i].ToString().Replace("A_Main", ma);
                    if (cca[i].ToString()!=ma)
                    {
                        cca[i] = cca[i].ToString().Replace("A_", aa);
                        if(Regex.Replace(cca[i].ToString(), @"[\d]", "") == aa&&num==1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + aj.ToString();
                            aj++;
                        }
                        else { }
                        cca[i] = cca[i].ToString().Replace("M_", ii);
                        if (Regex.Replace(cca[i].ToString(), @"[\d]", "") == ii&&num==1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + ij.ToString();
                            ij++;
                        }
                        else { }
                    }
                    else { }
                }
                else if (icca == 2)
                {
                    cca[i] = cca[i].ToString().Replace("main", ma);
                    if (cca[i].ToString() != ma)
                    {
                        cca[i] = cca[i].ToString().Replace("a", aa);
                        if (Regex.Replace(cca[i].ToString(), @"[\d]", "") == aa && num == 1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + aj.ToString();
                            aj++;
                        }
                        else { }
                        cca[i] = cca[i].ToString().Replace("i", ii);
                        if (Regex.Replace(cca[i].ToString(), @"[\d]", "") == ii && num == 1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + ij.ToString();
                            ij++;
                        }
                        else { }
                    }
                    else { } 
                }
                else if (icca == 3)
                {
                    cca[i] = cca[i].ToString().Replace("main_animation", ma);
                    if (cca[i].ToString() != ma)
                    {
                        cca[i] = cca[i].ToString().Replace("animation_", aa);
                        if (Regex.Replace(cca[i].ToString(), @"[\d]", "") == aa && num == 1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + aj.ToString();
                            aj++;
                        }
                        else { }
                        cca[i] = cca[i].ToString().Replace("sprite_", ii);
                        if (Regex.Replace(cca[i].ToString(), @"[\d]", "") == ii && num == 1)
                        {
                            cca[i] = Regex.Replace(cca[i].ToString(), @"[\d]", "") + ij.ToString();
                            ij++;
                        }
                        else { }
                    }
                    else { }
                }
                else { }
            }
            if (num == 1)
            {
                Console.WriteLine("元件序号重写完成");
            }
            else { }
        }
        catch
        {
            Console.WriteLine("ArrayListTransform ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }

    //生成元件转换部分
    public void ClipTransform(string Fpath)
    {
        try
        {
            //生成元件数组
            cr.ClipRead(Fpath);
            //得到检测的元件数组
            ca.AddRange(cr.ca);
            //复制数组
            cca.AddRange(ca);
            //选择转换形式
            Select(cca);
            if (icca != 0)
            {
                //转换行为
                ta.ClipTransform(Fpath + "\\LIBRARY", ca, cca);
                //DOMDocument重写
                ta.DOMDocumentTransform(Fpath + "\\DOMDocument.xml", about, ca, cca);
                //json重写
                ta.JsonRewrite(Fpath, oname, jname, j1, j2, j3, j4, j5, o1, o2, o3, o4, o5, icca, ca, cca);
            }
            else { }
        }
        catch
        {
            Console.WriteLine("ClipTransform ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }
}
