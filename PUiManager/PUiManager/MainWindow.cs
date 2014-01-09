using System;
using Gtk;

using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		UiManagerHelper uiManagerHelper = new UiManagerHelper(UIManager);
		
		ActionGroup actionGroup1 = new ActionGroup("pageActionGroup");
		Gtk.Action newAction = new Gtk.Action("newAction", null, null, Stock.New);
		actionGroup1.Add (newAction);
		Gtk.Action editAction = new Gtk.Action("editAction", null, null, Stock.Edit);
		actionGroup1.Add (editAction);
		
		ActionGroup actionGroup2 = new ActionGroup("pageActionGroup");
		Gtk.Action deleteAction = new Gtk.Action("deleteAction", null, null, Stock.Delete);
		actionGroup2.Add (deleteAction);

		ActionGroup currentActionGroup = actionGroup1;
		uiManagerHelper.SetActionGroup (currentActionGroup);
		
		
		executeAction.Activated += delegate {
			Console.WriteLine("executeAction.Activated");
			if (currentActionGroup == actionGroup1)
				currentActionGroup = actionGroup2;
			else
				currentActionGroup = actionGroup1;
			uiManagerHelper.SetActionGroup(currentActionGroup);
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
