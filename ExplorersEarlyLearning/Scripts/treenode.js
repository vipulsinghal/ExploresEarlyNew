$.fn.treeNode = function () {
    var handler = $(this);
    handler.find('.tree-icon').click(function () {
        $(this).siblings('ul').toggle('fast').parent().toggleClass('active');
    });
};

$.fn.mapItem = function () {
    var dom = $(this);
    var mapItemArrow = dom.find('.map-item .arrow');
    $(document).click(function () {
        mapItemArrow.siblings('ul').hide('fast');
    });

    return mapItemArrow.bind('click', function (e) {
        e.stopPropagation();
        var menu = $(this).siblings('ul');
        $('.map-item > ul:visible').not(menu).hide('fast'); menu.toggle('fast');
    });
};