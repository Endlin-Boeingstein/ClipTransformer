using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//建立判断类
class JudgeJoF
{
	//xfl文件夹路径定义
	public string Fpath = null;
	//创建ct实例
	public ClipTransformer ct = new ClipTransformer();
	//判断是否为dir路径
	public void Judge(string filepath)
    {
		try
		{
			if (File.Exists(filepath))
			{
				Console.WriteLine("已检测到为文件，而非xfl文件夹，请检查！");
				Console.WriteLine("请将文件夹拖入窗体，并按回车键");
				Judge(Console.ReadLine().Trim('"'));
			}
			else if (Directory.Exists(filepath))
			{
				Console.WriteLine("已检测到为xfl文件夹");
				this.Fpath = filepath;
				ct.ClipTransform(this.Fpath);
			}
			else
			{
				Console.WriteLine("未检测到文件或文件夹！请检查！");
				Console.WriteLine("请将文件夹拖入窗体，并按回车键");
				Judge(Console.ReadLine().Trim('"'));
			}
		}
		catch
		{
			Console.WriteLine("ERROR");
		}
	}
}
