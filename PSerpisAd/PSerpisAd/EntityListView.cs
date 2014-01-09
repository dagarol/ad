using Gtk;
using System;

namespace Serpis.Ad
{
	public class EntityListView : Bin, IEntityListView
	{

		protected TreeView treeView;
		protected ActionGroup actionGroup;
		public EntityListView () {
			
			SizeRequested += delegate(object o, SizeRequestedArgs args) {
				if (Child != null)
					args.Requisition = Child.SizeRequest ();
			};
			
			SizeAllocated += delegate(object o, SizeAllocatedArgs args) {
				if (Child != null)
					Child.Allocation = args.Allocation;
			};
			
			VBox vBox = new VBox();
			ScrolledWindow scrolledWindow = new ScrolledWindow();
			scrolledWindow.ShadowType = ShadowType.In;
			treeView = new TreeView();
			
			scrolledWindow.Add (treeView);
			vBox.Add (scrolledWindow);
			Add (vBox);
			
			ShowAll ();
			
			actionGroup = new ActionGroup("entityListView");			
		}
		
		public ActionGroup ActionGroup {
			get {return actionGroup;}
		}
	}
}

