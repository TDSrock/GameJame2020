using System;
namespace SjorsGielen.Helpers
{
    [Flags]
    public enum EditorListOption
    {
        None = 0,
        ListSize = 1,
        ListLabel = 2,
        ElementLabels = 4,
        Buttons = 8,
        NamedElementLabels = 16,
        ShowMultiListOverride = 32,
        ShowListSizeInLabel = 64,
        Default = ListSize | ListLabel | ElementLabels | ShowListSizeInLabel | Buttons,
        NoElementLabels = ListSize | ListLabel,
        All = ListSize | ListLabel | ElementLabels | Buttons | NamedElementLabels | ShowMultiListOverride | ShowListSizeInLabel
    }
}