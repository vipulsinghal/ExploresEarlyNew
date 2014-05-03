$.fn.dropdownMenu = function () { var dom = $(this); var dropdown = dom.find('.j_DropDown'); $(document).click(function () { dropdown.removeClass('active'); dropdown.children('.j_DropDownContent').slideUp('fast'); }); return dropdown.bind('click', function (e) { e.stopPropagation(); var o = $(this); var menu = o.children('.j_DropDownContent'); if (o.hasClass('active')) { o.removeClass('active'); menu.slideUp('fast'); } else { o.addClass('active'); menu.slideDown('fast'); } }); };
$.fn.dropdownButton = function () { var dom = $(this); var dropdown = dom.find('.dropdown-button'); $(document).click(function () { dropdown.removeClass('active'); dropdown.children('ul').slideUp('fast'); }); return dropdown.bind('click', function (e) { e.stopPropagation(); var o = $(this); var menu = o.children('ul'); if (o.hasClass('active')) { o.removeClass('active'); menu.slideUp('fast'); } else { o.addClass('active'); menu.slideDown('fast'); } }); };

$(function() {
    $(document).dropdownMenu();
    $(document).dropdownButton();
    $(document).mapItem();
});

var EX = {}
EX.Notify = function (Title, Desc) {
    new $.Zebra_Dialog('<strong>' + Title + '</strong><br/>' + Desc, {
        'buttons': false,
        'modal': false,
        'position': ['right - 20', 'top + 20'],
        'auto_close': 3000
    });
};