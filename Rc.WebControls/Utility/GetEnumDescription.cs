using System;


namespace Rc.WebControls.Utility {
	
	/// <summary>
	/// 获取指定枚举的说明文字
	/// </summary>
	public class EnumDescription {

		/// <summary>
		/// 获取指定枚举值的文字说明
		/// </summary>
		/// <param name="e">枚举值</param>
		/// <returns></returns>
		public static string GetDescription(object e) {
			//获取字段信息   
			System.Reflection.FieldInfo[] ms = e.GetType().GetFields();

			Type t = e.GetType();
			foreach (System.Reflection.FieldInfo f in ms) {
				//判断名称是否相等   
				if (f.Name != e.ToString()) continue;

				//反射出自定义属性   
				foreach (Attribute attr in f.GetCustomAttributes(true)) {
					//类型转换找到一个Description，用Description作为成员名称   
					System.ComponentModel.DescriptionAttribute dscript = attr as System.ComponentModel.DescriptionAttribute;
					if (dscript != null)
						return dscript.Description;
				}

			}

			//如果没有检测到合适的注释，则用默认名称   
			return e.ToString();   
		}
	}
}
