﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

//建立转换类
class TransformAction
{
    //生成元件转换部分
    public void ClipTransform(string Fpath,ArrayList ca,ArrayList cca,int graphic)
    {
        try
        {
            Console.WriteLine("元件转换中......");
            //创建路径文件夹实例
            DirectoryInfo TheFolder = new DirectoryInfo(Fpath);
            //创建文件数组
            FileInfo[] files = TheFolder.GetFiles();
            //为文件数组排序
            Array.Sort(files, new FileNameSort());
            //遍历文件夹内文件
            foreach (FileInfo NextFile in files)
            {
                //流式读取文件类型
                FileStream stream = new FileStream(NextFile.FullName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                string fileclass = "";
                try
                {
                    for (int i = 0; i < 2; i++)
                    {
                        fileclass += reader.ReadByte().ToString();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                stream.Close();
                //判定是否为xml
                if (fileclass == "6068")
                {
                    //读取xml
                    string xml = File.ReadAllText(NextFile.FullName);
                    //保存原xml值
                    string oxml = xml;
                    //读取名字
                    string xname = NextFile.Name;
                    //保存原xname值
                    string oxname = xname;
                    //替换引用和名字
                    //替换引用
                    //废弃代码，使用出错///for (int i = ca.Count-1; i >= 0; i--)
                    for (int i = 0; i <ca.Count ; i++)
                    {
                        xml = xml.Replace("\"" + ca[i].ToString() + "\"", "\"" + cca[i].ToString() + "\"");
                        //用了出错没替换完就跳出了//替换完毕就退出循环
                        //用了出错没替换完就跳出了///if (xml != oxml) break;
                    }
                    //替换名字
                    //废弃代码，使用出错///for (int i = ca.Count - 1; i >= 0; i--)
                    for (int i = 0; i < ca.Count; i++)
                    {
                        xname = xname.Replace(ca[i].ToString()+".xml", cca[i].ToString()+".xml");
                        //替换完毕就退出循环
                        if (xname != oxname) break;
                    }

                    //增加元件类型判断20240402添加
                    if (graphic == 0)
                    {
                        //删除原有图形类型
                        xml = xml.Replace(" symbolType=\"graphic\"", "");
                        //删除原有图形类型
                        xml = xml.Replace(" loop=\"loop\"", "");
                    }
                    else
                    {
                        //删除原有图形类型
                        xml = xml.Replace(" symbolType=\"graphic\"", "");
                        //删除原有图形类型
                        xml = xml.Replace(" loop=\"loop\"", "");
                        //全部增加图形类型引用
                        xml = xml.Replace("<DOMSymbolInstance", "<DOMSymbolInstance symbolType=\"graphic\" loop=\"loop\"");
                        //全部变成图形类型
                        xml = xml.Replace("<DOMSymbolItem", "<DOMSymbolItem symbolType=\"graphic\"");
                    }
                    

                    //输出文本
                    File.WriteAllText(Fpath + "\\" + xname, xml);
                    if (Fpath + "\\" + xname != NextFile.FullName)
                    {
                        //删除对应旧元件
                        File.Delete(Fpath + "\\" + NextFile.Name);
                    }
                }
                else { }
            }
            Console.WriteLine("元件转换完成");
        }
        catch
        {
            Console.WriteLine("TransformActionClipTransform ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }
    //生成DOMDocument转换部分
    public void DOMDocumentTransform(string DPath,string about,ArrayList ca,ArrayList cca,int graphic)
    {
        try
        {
            Console.WriteLine("DOMDocument重写中......");
            //读取xml
            string xml = File.ReadAllText(DPath);
            //替换about
            xml = xml.Replace("This XFL is convert from PAM file, By SPC-Util.", about);
            xml = xml.Replace("this XFL is convert from Popcap-AniMation file , by TaiJi .", about);
            //替换引用
            for (int i = 0; i < ca.Count; i++)
            {
                xml = xml.Replace("\"" + ca[i].ToString() + "\"", "\"" + cca[i].ToString() + "\"");
                xml = xml.Replace("\"" + ca[i].ToString() + ".xml\"", "\"" + cca[i].ToString() + ".xml\"");
            }

            //增加元件类型判断20240402添加
            if (graphic == 0)
            {
                //删除原有图形类型
                xml = xml.Replace(" symbolType=\"graphic\"", "");
                //删除原有图形类型
                xml = xml.Replace(" loop=\"loop\"", "");
            }
            else
            {
                //删除原有图形类型
                xml = xml.Replace(" symbolType=\"graphic\"", "");
                //删除原有图形类型
                xml = xml.Replace(" loop=\"loop\"", "");
                //全部增加图形类型引用
                xml = xml.Replace("<DOMSymbolInstance", "<DOMSymbolInstance symbolType=\"graphic\" loop=\"loop\"");
            }
            

            //输出文本
            File.WriteAllText(DPath, xml);
            Console.WriteLine("DOMDocument重写完成");
        }
        catch
        {
            Console.WriteLine("DOMDocumentTransform ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }
    //生成json重写部分
    public void JsonRewrite(string Fpath,string oname,string jname,string j1,string j2,string j3,string j4,string j5,string o1,string o2,string o3,string o4,string o5,int icca,ArrayList ca,ArrayList cca)
    {
        try
        {
            Console.WriteLine("json重写中......");
            //读取json
            string json = File.ReadAllText(Fpath + "\\" + oname);
            //替换
            json = json.Replace("\""+o1+ "\"", "\""+j1+ "\"");
            json = json.Replace("\""+o2+ "\"", "\""+j2+ "\"");
            json = json.Replace("\""+o3+ "\"", "\""+j3+ "\"");
            json = json.Replace("\""+o4+ "\"", "\""+j4+ "\"");
            json = json.Replace("\""+o5+ "\"", "\""+j5+ "\"");
            //替换引用和名字
            for (int i = 0; i < ca.Count; i++)
            {
                json = json.Replace("\""+ca[i].ToString()+ "\"", "\""+cca[i].ToString()+ "\"");
            }
            //输出文本
            File.WriteAllText(Fpath + "\\" + jname, json);
            if (jname != oname)
            {
                //删除对应旧json
                File.Delete(Fpath + "\\" + oname);
            }
            else { }
            Console.WriteLine("json重写完成");
        }
        catch
        {
            Console.WriteLine("JsonRewrite ERROR");
            //提示按任意键继续
            Console.WriteLine("Press any key to continue...");
            //输入任意键退出
            Console.ReadLine();
        }
    }
}
