// npm package: flatpickr
// github link: https://github.com/flatpickr/flatpickr

$(function () {
    'use strict';

    // date picker 
    if ($('.flatpickr-date').length) {
        flatpickr(".flatpickr-date", {
            wrap: true,
            dateFormat: "Y-m-d",
        });
    }


    // time picker
    if ($('#flatpickr-time').length) {
        flatpickr("#flatpickr-time", {
            wrap: true,
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
        });
    }

    //if ($('#flatpickr-month').length) {
    //    flatpickr("#flatpickr-month", {
    //        plugins: [
    //            new monthSelectPlugin({
    //                shorthand: true, //defaults to false
    //                dateFormat: "m.y", //defaults to "F Y"
    //                altFormat: "F Y", //defaults to "F Y"
    //                theme: "dark" // defaults to "light"
    //            })
    //        ]
    //    });
    //}
    if ($('#additionaldate-time').length) {
        flatpickr("#additionaldate-time", {
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
            minTime: "16:00",
            maxTime: "22:30",
    });
  }

});