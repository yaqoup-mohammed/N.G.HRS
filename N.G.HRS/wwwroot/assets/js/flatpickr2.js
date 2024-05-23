// commonjs
//const flatpickr = require("flatpickr");

// es modules are recommended, if available, especially for typescript
//import flatpickr from "../vendors/flatpickr/";

//flatpickr(".flatpickr-date1", {
//    altInput: true,
//    altFormat: "F j, Y",
//    dateFormat: "Y-m-d",
//});
//flatpickr(".flatpickr-Time1", {
//    wrap: true,
//    enableTime: true,
//    noCalendar: true,
//    time_24hr: false,
//    dateFormat: "h:i",
//    dateFormat: "h:i K",
//});
// npm package: flatpickr
// github link: https://github.com/flatpickr/flatpickr
//import { monthSelectPlugin } from 'src/plugins/monthSelect/index.js';
//import flatpickr from 'src/styles/flatpickr.min.css';
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
    if ($('#flatpickr-time1').length) {
        flatpickr("#flatpickr-time1", {wrap: true,enableTime: true,noCalendar: true,time_24hr: false,dateFormat: "h:i K", });
    }
    if ($('#flatpickr-time').length) {
        flatpickr("#flatpickr-time", {wrap: true,enableTime: true,noCalendar: true,time_24hr: false,dateFormat: "h:i K", });
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
            time_24hr: false,
            dateFormat: "h:i K",
            //dateFormat: "h:i",            
        });
    }

});