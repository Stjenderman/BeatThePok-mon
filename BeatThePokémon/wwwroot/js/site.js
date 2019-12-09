// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//js voor create
$(function () {
    $(".SoortButton").click(
        function () {
            SetType("#TypeText", $(this).data("type"));
            DeselectAllButtons();
            SelectAllButtons(this);
        }
    );
});


function SetType(typeText, type) {
    $(typeText).val(type);
}

function DeselectAllButtons() {
    $('.SoortButton').css('border', '');
}

function SelectAllButtons(button) {
    $(button).css('border', 'darkgrey 3px solid');
}


//js voor layout

$(".hover-slidedown").hover(
    function () { $(".nav-transformation").stop().slideDown(500) },
    function () { $(".nav-transformation").stop().slideUp(500) }
);

$(".poke-nav-btn").hover(
    function () { $(".poke-dropdown-div").stop().slideDown(500) },
    function () { $(".poke-dropdown-div").stop().slideUp(500) }
);