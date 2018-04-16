using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 自定义的CheckBoxList
	/// </summary>
	public class CheckBoxList : System.Web.UI.WebControls.CheckBoxList, IDataBindControls {

		#region 属性定义

		/// <summary>
		/// 是否已经添加过项目，因为可能用户会强制刷新控件，这个开关防止重复添加。
		/// </summary>
		private const string IS_ADDED_LIST_ITEM = "is_added_list_item";

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public CheckBoxList() { }

		private bool _isRequired = false;
		/// <summary>
		/// 该控件是否必须选择项目
		/// </summary>
		public bool IsRequired {
			get { return _isRequired; }
			set { _isRequired = value; }
		}

		private ShowErrorTypes _showErrorType = ShowErrorTypes.Inline;
		/// <summary>
		/// 数据错误验证错误信息提示方式
		/// </summary>
		public ShowErrorTypes ShowErrorType {
			get { return _showErrorType; }
			set { _showErrorType = value; }
		}

		private string _requiredErrorMessage = "请选择";
		/// <summary>
		/// 没有选择任何项目是的错误信息
		/// </summary>
		public string RequiredErrorMessage {
			get { return _requiredErrorMessage; }
			set { _requiredErrorMessage = value; }
		}

		#endregion

		#region 重写方法

		/// <summary>
		/// 创建子控件方法
		/// </summary>
		protected override void CreateChildControls() {
			if (this.ViewState[IS_ADDED_LIST_ITEM] == null) {
				if (IsRequired) {
					this.Attributes.Add("IsRequired", "True");
				}
				this.Attributes.Add("MyInputType", "CheckBoxList");
				this.Attributes.Add("ShowErrorType", ShowErrorType.ToString());
				AddDropDownItem();		//添加列表项目重载
			}
			
			base.CreateChildControls();
		}

		/// <summary>
		/// 重写render
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) {
			base.Render(writer);
			writer.WriteLine();
			if (this.IsRequired == true) {
				writer.Write("<span class='span_required_check' style='display:none;' id='span_required_check_for_{0}'>{1}</span>", this.ClientID, RequiredErrorMessage);
			}
		}

		#endregion

		#region 扩展方法

		/// <summary>
		/// 获取选则项目GUID值
		/// </summary>
		public Guid SelectedGuidValue {
			get {
				if (string.IsNullOrEmpty(this.SelectedValue))
					return Guid.Empty;
				else
					return new Guid(this.SelectedValue);
			}
			set {
				if (value == Guid.Empty)
					this.SelectedValue = string.Empty;
				else
					this.SelectedValue = value.ToString();
			}
		}

		/// <summary>
		/// 添加子项目
		/// </summary>
		protected virtual void AddDropDownItem() {
			this.ViewState[IS_ADDED_LIST_ITEM] = "true";
		}

		/// <summary>
		/// 重新加载列表项
		/// </summary>
		public void ReLoadItem() {
			this.Items.Clear();
			this.ViewState[IS_ADDED_LIST_ITEM] = null;
			AddDropDownItem();
		}

		#endregion

		#region 数据验证部分

		/// <summary>
		/// 控件数据是否通过验证
		/// </summary>
		public bool IsValid {
			get {
				return DataValid();
			}
		}

		/// <summary>
		/// 验证数据
		/// </summary>
		/// <returns></returns>
		protected virtual bool DataValid() {

			return true;
		}

		#endregion
	}
}
