using Rc.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 自定义的DrugDownList
	/// </summary>
	public class DropDownList : System.Web.UI.WebControls.DropDownList, IDataBindControls {

		#region 定义属性

		/// <summary>
		/// 是否已经添加过项目，因为可能用户会强制刷新控件，这个开关防止重复添加。
		/// </summary>
		private const string IS_ADDED_LIST_ITEM = "is_added_list_item";

		private bool _isRequired = false;
		private bool _isAddEmptyItem = false;
		private EmptyItemTypes _emptyItemType;

		/// <summary>
		/// 空白项目的名称
		/// </summary>
		public string EmptyItemText { get; set; }

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DropDownList() {
			_emptyItemType = EmptyItemTypes.Choice;
		}

		/// <summary>
		/// 设置或获取空白数据项目的类型
		/// </summary>
		public EmptyItemTypes EmptyItemType {
			get { return _emptyItemType; }
			set { _emptyItemType = value; }
		}

		/// <summary>
		/// 该控件是否必须选择项目
		/// </summary>
		public bool IsRequired {
			get { return _isRequired; }
			set { _isRequired = value; }
		}

		/// <summary>
		/// 是否在最前面添加空白项目
		/// </summary>
		public bool IsAddEmptyItem {
			get { return _isAddEmptyItem; }
			set { _isAddEmptyItem = value; }
		}

		private string _requiredErrorMessage = "请选择";
		/// <summary>
		/// 没有选择任何项目是的错误信息
		/// </summary>
		public string RequiredErrorMessage {
			get { return _requiredErrorMessage; }
			set { _requiredErrorMessage = value; }
		}

		private ShowErrorTypes _showErrorType = ShowErrorTypes.Inline;
		/// <summary>
		/// 数据错误验证错误信息提示方式
		/// </summary>
		public ShowErrorTypes ShowErrorType {
			get { return _showErrorType; }
			set { _showErrorType = value; }
		}

		#endregion

		#region 重写方法

		/// <summary>
		/// 创建子控件方法
		/// </summary>
		protected override void CreateChildControls() {
			if (IsRequired) {
				this.Attributes.Add("IsRequired", "True");
			}
			this.Attributes.Add("MyInputType", "DropDownList");
			this.Attributes.Add("ShowErrorType", ShowErrorType.ToString());

			if (this.ViewState[IS_ADDED_LIST_ITEM] == null) {
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
				if (string.IsNullOrEmpty(SelectedValue))
					return Guid.Empty;
				else
					return new Guid(SelectedValue);
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
			if (_isAddEmptyItem && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
				if (!string.IsNullOrWhiteSpace(EmptyItemText)) {
					this.Items.Insert(0, new System.Web.UI.WebControls.ListItem(EmptyItemText, string.Empty));
				}
				else {
					if (EmptyItemType == EmptyItemTypes.Choice) {
						this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--请选择--", string.Empty));
					}
					else {
						this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--全 部--", string.Empty));
					}
				}
			}
			this.ViewState[IS_ADDED_LIST_ITEM] = "true";
		}

		/// <summary>
		/// 设置或获取选中项目的值
		/// </summary>
		public override string SelectedValue {
			get {
				if (Page != null && !Page.IsPostBack && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
					AddDropDownItem();
				}
				return base.SelectedValue;
			}
			set {
				if (Page != null && !Page.IsPostBack && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
					AddDropDownItem();
				}
				base.SelectedValue = value;
			}
		}

		/// <summary>
		/// 获取当前选中项
		/// </summary>
		public override System.Web.UI.WebControls.ListItem SelectedItem {
			get {
				if (Page != null && !Page.IsPostBack && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
					AddDropDownItem();
				}
				return base.SelectedItem;
			}
		}

		/// <summary>
		/// 设置或获取选中项目索引
		/// </summary>
		public override int SelectedIndex {
			get {
				if (Page != null && !Page.IsPostBack && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
					AddDropDownItem();
				}
				return base.SelectedIndex;
			}
			set {
				if (Page != null && !Page.IsPostBack && this.ViewState[IS_ADDED_LIST_ITEM] == null) {
					AddDropDownItem();
				}
				base.SelectedIndex = value;
			}
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
