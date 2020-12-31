/*
Thanks to Chris Sainty for providing an article and source on Fragment routing in Blazor
https://chrissainty.com/fragment-routing-with-blazor/ 
 */
window.blazorHelpers = {
    scrollToFragment: (elementId) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({
                behavior: "auto"
            });
        }
    }
};