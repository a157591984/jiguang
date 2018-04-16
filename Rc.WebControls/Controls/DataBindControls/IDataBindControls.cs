using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 所有数据绑定控件的默认接口
	/// </summary>
	public interface IDataBindControls {

		/// <summary>
		/// 清空并重新加载数据项
		/// </summary>
		void ReLoadItem();

	}
}
