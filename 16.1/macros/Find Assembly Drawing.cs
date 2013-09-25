namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            akit.Callback("gdr_menu_select_active_draw", "", "main_frame");				// opens drawing list
            akit.PushButton("dia_draw_display_all", "Drawing_selection"); 				// show all drawings
			akit.ValueChange("Drawing_selection", "diaSavedSearchOptionMenu", "0");     // resets the filter
			akit.ValueChange("Drawing_selection", "diaSavedSearchOptionMenu", "10");	// filters assembly drawings
            akit.PushButton("dia_draw_filter_by_parts", "Drawing_selection");			// filters by parts
            akit.TableSelect("Drawing_selection", "dia_draw_select_list", 1);			// selects the first drawing in the list
            akit.PushButton("dia_draw_open", "Drawing_selection");						// opens the drawing
        }
    }
}
