// Based on examples from @dabeng.  Note that these URLs all point into the OrgChartConroller in the
// web api.
var ajaxURLs = {
    'children': '/orgchart/children/',
    'parent': '/orgchart/parent/',
    'siblings': function (nodeData) {
        return '/orgchart/siblings/' + nodeData.id;
    },
    'families': function (nodeData) {
        return '/orgchart/families/' + nodeData.id;
    }
};

$('#chart-container').orgchart({
    'data': 'orgchart/initdata',
    'ajaxURL': ajaxURLs,
    'nodeContent': 'title',
    'nodeId': 'id',
    'toggleSiblingsResp': true,
    'pan': true,

    'createNode': function ($node, data) {
        var secondMenuIcon = $('<i>', {
            'class': 'fa fa-info-circle second-menu-icon',
            click: function () {
                $(this).siblings('.second-menu').toggle();
            }
        });
        var secondMenu = '<div class="second-menu">';
        if (data.photourl) {
            secondMenu = secondMenu + '<img class="avatar" src="' + data.photourl + '">';
        }
        secondMenu = secondMenu + '<div><strong>' + data.name + '</strong></div>'
        secondMenu = secondMenu + '<div>' + data.title + '</div>'
        if (data.divison || data.office) {
            let divison = data.divison;
            if (!divison) { divison = "" };
            let office = data.office;
            if (!office) { office = "" };
            secondMenu = secondMenu + '<div><strong>' + divison + '</strong> ' + office + '</div>'
        }
        secondMenu = secondMenu + '</div>';
        $node.append(secondMenuIcon).append(secondMenu);
    }


});