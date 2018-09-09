using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.ViewModels.Translator
{
    public interface IViewModelViewTranslator
    {
        Type GetViewModelTypeForView(Type view_type);
        Type GetViewTypeForViewModel(Type view_type);
    }
}
