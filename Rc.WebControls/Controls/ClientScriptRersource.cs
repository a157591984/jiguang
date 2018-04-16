using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {	
	
	/// <summary>
	/// 自定义的脚本资源
	/// </summary>
	[System.Web.AspNetHostingPermission(System.Security.Permissions.SecurityAction.Demand,
		Level = System.Web.AspNetHostingPermissionLevel.Minimal)]
	public class ClientScriptRersource : System.Web.UI.Control {

		/// <summary>
		/// 是否包含Jquery脚本
		/// </summary>
		public bool IsIncludeJqueryScript { get; set; }

		/// <summary>
		/// 改写onprerender
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			if (this.Page != null) {
				if (IsIncludeJqueryScript) {
					this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "Rc.WebControls.Script.jquery1.10.2.js");
				}

				this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "Rc.WebControls.Script.DataValid.js");
				this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "Rc.WebControls.Script.JqueryCustomDialog.js");
			}
			base.OnPreRender(e);
		}

	}
}
