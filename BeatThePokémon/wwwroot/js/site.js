// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//js voor buttons
$(function () {
    $(".SoortButton").click(
        function () {
            SetType("#TypeText", $(this).data("type"));
            DeselectAllTypeButtons();
            SelectAllButtons(this);
        }
    );
});

$(function () {
    $(".choose-button").click(
        function () {
            SetType(".hidden-pokemon", $(this).data("pokemon"));
            DeselectAllPokemonButtons();
            SelectAllButtons(this);
            ShowButton('.confirm-btn');
        }
    );
});

function SetType(typeText, type) {
    $(typeText).val(type);
}

function DeselectAllTypeButtons() {
    $('.SoortButton').css('border', '');
}

function DeselectAllPokemonButtons() {
    $('.choose-button').css('border', '');
}

function SelectAllButtons(button) {
    $(button).css('border', 'darkgrey 3px solid');
}

function ShowButton(button) {
    $(button).fadeIn(100);
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

