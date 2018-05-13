
//Index
function Click() {
    $('#test').val(navigator.userAgent);
}

function MoreClick(name) {
    if (name == 'coupons') {
        document.location.href = '/Home/Commodity';
    } else if (name == 'article') {
        document.location.href = '/Home/Article'
    } else if (name == 'pictures') {
        document.location.href = '/Home/Pictures'
    }
}

//Commodity
function ShowCopy(i) {
    $('#showcopy_' + i).css('display', 'block');
}

function HideCopy(i) {
    $('#showcopy_' + i).css('display', 'none');
    document.getElementById('copytext_' + i).innerText = '复制文案';
    clearSlct();
}

function ShowText(i) {
    $('#board_' + i).css('display', 'block');

    var divborder = document.getElementById('board_' + i).getBoundingClientRect();
    var width = window.innerWidth;
    var result = parseInt(width) - parseInt(divborder.right);

    if (result < 0) {
        $('#board_' + i).css('right', '255px');
    }
}

function HideText(i) {
    $('#board_' + i).css('display', 'none');
}

function CopyClick(i) {
    var clipboard = new ClipboardJS('#showcopy_' + i, {
        target: function () {
            return document.querySelector('#board_' + i);
        }
    });

    document.getElementById('copytext_' + i).innerText = '已复制';
}

function Search() {
    var s = $('#search').val();
    document.location.href = '/Home/Commodity?search=' + s;
}

var clearSlct = "getSelection" in window ? function () {
    window.getSelection().removeAllRanges();
} : function () {
    document.selection.empty();
    };

