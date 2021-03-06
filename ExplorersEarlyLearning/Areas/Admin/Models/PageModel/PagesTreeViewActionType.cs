﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.PageModel
{
    public enum PagesTreeViewActionType
    {
        AddPage,
        EditPage,
        UpdatePage,
        DeletePage,
        PreviewPage,
        MoveDown,
        MoveUp,
        PublishPage,
        UnpublishPage,
        AddChild
    }
}