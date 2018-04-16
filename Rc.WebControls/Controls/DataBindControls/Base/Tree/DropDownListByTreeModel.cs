using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Rc.WebControls {
	
	/// <summary>
	/// 用DropDownList显示树状数据
	/// </summary>
	public class DropDownListByTreeModel : DropDownList {

		private IList<TreeNodeInfo> _itemsSources = new List<TreeNodeInfo>();
		/// <summary>
		/// 设置或获取当前数据源的集合
		/// </summary>
		public IList<TreeNodeInfo> ItemsSources {
			get { return _itemsSources; }
			set { _itemsSources = value; }
		}

		/// <summary>
		/// 是否允许选择父节点
		/// </summary>
		public bool IsEnableSelectParenNode { get; set; }

		/// <summary>
		/// 过滤的节点ID，如果设置了，只有本节点的下级几点才会显示
		/// </summary>
		public string FielterNodeValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		protected override void AddDropDownItem() {

			if (IsEnableSelectParenNode) {
				BuildByAllNode();
			}
			else {
				BuildByLeafNode();
			}

			base.AddDropDownItem();
		}

		#region 生成包括中间节点和叶子节点的下拉列表
		private void BuildByAllNode() {
			var rootNodes = new List<TreeNodeInfo>();
			if (string.IsNullOrWhiteSpace(FielterNodeValue)) {
				rootNodes = _itemsSources.Where(p => string.IsNullOrWhiteSpace(p.ParentNodeID)).ToList();
			}
			else {
				rootNodes = _itemsSources.Where(p => p.ParentNodeID == FielterNodeValue).ToList();
			}

			rootNodes.OrderByDescending(p => p.Priority);
			foreach (var rootNode in rootNodes) {
				this.Items.Add(new ListItem(rootNode.NodeName, rootNode.NodeID));
				GetSubListItem(rootNode.NodeID, rootNode.NodeName);
			}
		}

		private void GetSubListItem(string parentId, string parentPath) {
			var subItems = _itemsSources.Where(p => p.ParentNodeID == parentId);
			subItems.OrderByDescending(p => p.Priority);
			foreach (var subItem in subItems) {
				var subParentPath = string.Format("{0}->{1}", parentPath, subItem.NodeName);
				this.Items.Add(new ListItem(subParentPath, subItem.NodeID));
				GetSubListItem(subItem.NodeID, subParentPath);
			}
		}

		#endregion

		#region 生成只显示叶子节点的下拉列表项目

		private void BuildByLeafNode() {
			//重构传入的数据结构
			var parentIds = _itemsSources.Select(p => p.ParentNodeID);
			var infoes = _itemsSources.Where(p => !parentIds.Contains(p.NodeID)).ToList();			//获取叶子节点

			for (int i = 0; i < infoes.Count; i++) {
				bool isFindFielterNodeValue = false;
				infoes[i].NodeName = GetModeParentPath(infoes[i], ref isFindFielterNodeValue);
				//如果设置了按节点过滤，那么如果不包含，那么就移除节点
				if (!string.IsNullOrWhiteSpace(FielterNodeValue) && !isFindFielterNodeValue) {
					infoes.RemoveAt(i);
					i--;
				}
			}

			var infoes1 = infoes.OrderBy(p => p.NodeName);

			foreach (var info in infoes1) {
				this.Items.Add(new ListItem(info.NodeName, info.NodeID));
			}
		}

		private string GetModeParentPath(TreeNodeInfo treeInfo, ref bool isFindFielterNodeValue) {

			var infoes = _itemsSources.Where(p => p.NodeID == treeInfo.ParentNodeID).ToArray();
			if (infoes.Count() == 1) {
				if (!isFindFielterNodeValue) {
					isFindFielterNodeValue = (infoes[0].NodeID == FielterNodeValue);
					if (isFindFielterNodeValue) {			//如果找到限制节点，那么停止递归
						return treeInfo.NodeName;
					}
				}
				return string.Format("{0}->{1}", GetModeParentPath(infoes[0], ref isFindFielterNodeValue), treeInfo.NodeName);
			}
			else {
				return treeInfo.NodeName;
			}
		}

		#endregion
	}
}
