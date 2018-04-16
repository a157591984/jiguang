using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 自定义的Repeater
	/// </summary>
	public class RepeaterPaddingBar : System.Web.UI.WebControls.CompositeControl {

		private const string VIEWSTATE_PAGE_COUNT = "viewstate_Page_Count";

		private Label _lblPageInfo;
		private System.Web.UI.WebControls.LinkButton _lbtnPre;
		private System.Web.UI.WebControls.LinkButton _lbtnNext;
		private System.Web.UI.WebControls.LinkButton _lbtnFirst;
		private System.Web.UI.WebControls.LinkButton _lbtnEnd;
		private TextBoxInt _txtPageIndex;
		private TextBoxInt _txtPageSize;
		private int _recordsCount;
		private string _idName;
		private bool _isChanged;
		private System.Web.UI.WebControls.Button _btnSearch;


		/// <summary>
		/// 构造函数
		/// </summary>
		public RepeaterPaddingBar() {
			this.CssClass = "repeaterPaddingBar";
		}

		#region 定义属性

		/// <summary>
		/// 每页记录的数量
		/// </summary>
		public int PageSize {
			get {
				EnsureChildControls();
				return _txtPageSize.TextInt;
			}
			set {
				EnsureChildControls();
				_txtPageSize.TextInt = value;
			}
		}

		/// <summary>
		/// 设置或设置当前的页码
		/// </summary>
		public int PageIndex {
			get {
				EnsureChildControls();
				return _txtPageIndex.TextInt - 1;
			}
			set {
				EnsureChildControls();
				_txtPageIndex.TextInt = value + 1;
				ChangedPageIndexed(value);
			}
		}

		private void ChangedPageIndexed(int pageIndex) {
			//设置导航按钮
			if (pageIndex > 0) {
				_lbtnFirst.Enabled = true;
				_lbtnPre.Enabled = true;
				_lbtnFirst.CssClass = "fenye_btn";
				_lbtnPre.CssClass = "fenye_btn";
			}
			else {
				_lbtnPre.Enabled = false;
				_lbtnFirst.Enabled = false;
				_lbtnPre.CssClass = "fenye_btn2";
				_lbtnFirst.CssClass = "fenye_btn2";
			}

			if (pageIndex < (int)this.ViewState[VIEWSTATE_PAGE_COUNT] - 1) {
				_lbtnEnd.Enabled = true;
				_lbtnNext.Enabled = true;
				_lbtnEnd.CssClass = "fenye_btn";
				_lbtnNext.CssClass = "fenye_btn";
			}
			else {
				_lbtnNext.Enabled = false;
				_lbtnEnd.Enabled = false;
				_lbtnNext.CssClass = "fenye_btn2";
				_lbtnEnd.CssClass = "fenye_btn2";
			}
		}
		/// <summary>
		/// 设置或获取符合条件的记录数量
		/// </summary>
		public int RecordCount {
			get {
				EnsureChildControls();
				return _recordsCount;
			}
			set {
				EnsureChildControls();
				_recordsCount = value;

				int pageCount = (_recordsCount - 1) / PageSize + 1;
				this.ViewState[VIEWSTATE_PAGE_COUNT] = pageCount;				

				_lblPageInfo.Text = string.Format("　共{0}条数据　第{1}页/共{2}页　", _recordsCount, PageIndex + 1, pageCount);
				ChangedPageIndexed(_txtPageIndex.TextInt - 1);
			}
		}

		#endregion

		#region 定义事件

		private static readonly object objEvent = new object();

		/// <summary>当栏目树选择的节点发生改变</summary>
		public event EventHandler ChangePageIndex {
			add { Events.AddHandler(objEvent, value); }
			remove { Events.RemoveHandler(objEvent, value); }
		}

		/// <summary>
		/// 用户登录时触发此事件
		/// </summary>
		/// <param name="e">参数</param>
		/// <param name="sender">控件</param>
		protected virtual void OnChangePageIndex(object sender, EventArgs e) {
			if (_isChanged)
				return;

			if (sender is System.Web.UI.WebControls.LinkButton) {
				switch (((System.Web.UI.WebControls.LinkButton)sender).CommandName) {
					case "pre":
						_txtPageIndex.TextInt -= 1;
						break;
					case "next":
						_txtPageIndex.TextInt += 1;
						break;
					case "first":
						_txtPageIndex.TextInt = 1;
						break;
					case "end":
						_txtPageIndex.TextInt = (int)this.ViewState[VIEWSTATE_PAGE_COUNT];
						break;
				}
			}
			//如果用户输入的页码大于总页码数量，那么自动便成为最大值
			if (_txtPageIndex.TextInt > (int)this.ViewState[VIEWSTATE_PAGE_COUNT]) {
				_txtPageIndex.TextInt = (int)this.ViewState[VIEWSTATE_PAGE_COUNT];
			}

			EventHandler handler = Events[objEvent] as EventHandler;
			if (handler != null)
				handler(this, e);

			RaiseBubbleEvent(this, e);

			_isChanged = true;
		}

		/// <summary>
		/// 事件冒泡
		/// </summary>
		/// <param name="source">控件</param>
		/// <param name="args">参数</param>
		/// <returns></returns>
		protected override bool OnBubbleEvent(object source, EventArgs args) {
			bool handled = false;

			OnChangePageIndex(source, args);
			handled = true;

			return handled;
		}

		#endregion

		#region 定义方法

		/// <summary>
		/// 默认顶顶级Tag类型
		/// </summary>
		protected override System.Web.UI.HtmlTextWriterTag TagKey {
			get {
				return System.Web.UI.HtmlTextWriterTag.Div;
			}
		}

		/// <summary>
		/// 创建子控件方法
		/// </summary>
		protected override void CreateChildControls() {
			_idName = this.ClientID + "repeater";

			this.ID = _idName;

			_lbtnFirst = new System.Web.UI.WebControls.LinkButton();
			_lbtnFirst.Text = "首页";
			_lbtnFirst.Click += new EventHandler(OnChangePageIndex);
			_lbtnFirst.CommandName = "first";
			_lbtnFirst.CssClass = "fenye_btn2";
			this.Controls.Add(_lbtnFirst);

			_lbtnPre = new System.Web.UI.WebControls.LinkButton();
			_lbtnPre.Text = "上一页";
			_lbtnPre.Click += new EventHandler(OnChangePageIndex);
			_lbtnPre.CommandName = "pre";
			_lbtnPre.CssClass = "fenye_btn2";
			this.Controls.Add(_lbtnPre);

			_lblPageInfo = new Label();
			this.Controls.Add(_lblPageInfo);

			_lbtnNext = new System.Web.UI.WebControls.LinkButton();
			_lbtnNext.Text = "下一页";
			_lbtnNext.Click += new EventHandler(OnChangePageIndex);
			_lbtnNext.CommandName = "next";
			_lbtnNext.CssClass = "fenye_btn2";
			this.Controls.Add(_lbtnNext);

			_lbtnEnd = new System.Web.UI.WebControls.LinkButton();
			_lbtnEnd.Text = "末页";
			_lbtnEnd.Click += new EventHandler(OnChangePageIndex);
			_lbtnEnd.CommandName = "end";
			_lbtnEnd.CssClass = "fenye_btn2";
			this.Controls.Add(_lbtnEnd);

			var label = new Label();
			label.Text = " 每页";
			this.Controls.Add(label);

			_txtPageSize = new TextBoxInt();
			_txtPageSize.MaxLength = 3;
			//_txtPageSize.ID = "txtPageSize";
			_txtPageSize.CssClass = "fenye_pageSize";
			this.Controls.Add(_txtPageSize);

			label = new Label();
			label.Text = "页　跳至第";
			this.Controls.Add(label);

			_txtPageIndex = new TextBoxInt();
			_txtPageIndex.ID = "txtPageIndex";
			_txtPageIndex.TextInt = 1;
			_txtPageIndex.MaxLength = 4;
			_txtPageIndex.CssClass = "fenye_pageIndex";
			this.Controls.Add(_txtPageIndex);

			label = new Label();
			label.Text = "页";
			this.Controls.Add(label);

			_btnSearch = new System.Web.UI.WebControls.Button();
			_btnSearch.Text = "Go";
			_btnSearch.CommandName = "btnGo";
			_btnSearch.CssClass = "fenye_go";
			_btnSearch.Click += new EventHandler(OnChangePageIndex);
			this.Controls.Add(_btnSearch);

			base.CreateChildControls();
		}

		#endregion
	}
}
