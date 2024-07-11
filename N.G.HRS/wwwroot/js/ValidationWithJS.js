
class ValidationFromMe {

    //To Convert From 12 To 24 Hour Format 
    convertTimeTo12(timeString) {
        const [hours, minutes] = timeString.split(':');
        let convertedHours = hours % 12;
        convertedHours = convertedHours ? convertedHours : 12; // Handle midnight as 12 AM
        const ampm = hours >= 12 ? 'PM' : 'AM';
        return `${ampm} ${convertedHours}:${minutes}`;
    }
    //=================================================
    // To Check If Start Time Is After End Time
    isStartTimeAfterEndTime(startTime, endTime) {
        // Convert start and end times to 24-hour format for easier comparison
        const startTime24 = this.convertTo24HourFormat(startTime);
        const endTime24 = this.convertTo24HourFormat(endTime);

        // Compare times
        return startTime24 > endTime24;
    }
    //==============
    //To calculate Days 
    calculateDaysDifference(startDate, endDate) {
        // Convert both dates to milliseconds
        const startMillis = new Date(startDate).getTime();
        const endMillis = new Date(endDate).getTime();

        // Calculate the difference in milliseconds
        const differenceMillis = Math.abs(endMillis - startMillis);

        // Convert milliseconds to days
        const daysDifference = Math.ceil(differenceMillis / (1000 * 60 * 60 * 24));

        return daysDifference;
    }
    //==============
    //If the time is needs to convert to 24 system first

    calculateHoursBetweenWith24(startTime, endTime) {
        // Convert start and end times to 24-hour format for easier calculation
        const startTime24 = this.convertTo24HourFormat(startTime);
        const endTime24 = this.convertTo24HourFormat(endTime);

        // Calculate the difference in minutes
        let minutesDifference = endTime24 - startTime24;

        // If the difference is negative, add 24 hours to it
        if (minutesDifference < 0) {
            minutesDifference += 24 * 60; // 24 hours in minutes
        }
        // Convert minutes to hours and minutes
        const hours = Math.floor(minutesDifference / 60);
        const minutes = minutesDifference;
        return { hours, minutes };
    }
    //==============
    //If the time is alrady with 24 system
    //===================================================
    calculateMinuteDifference(startTime, endTime) {
        // Split the time strings into hours and minutes
        const startParts = startTime.split(':');
        const endParts = endTime.split(':');

        // Convert hours and minutes to integers
        const startHour = parseInt(startParts[0], 10);
        const startMinute = parseInt(startParts[1], 10);
        const endHour = parseInt(endParts[0], 10);
        const endMinute = parseInt(endParts[1], 10);

        // Calculate the total minutes for start and end times
        const totalStartMinutes = (startHour * 60) + startMinute;
        const totalEndMinutes = (endHour * 60) + endMinute;

        // Calculate the difference in minutes
        let minuteDifference = totalEndMinutes - totalStartMinutes;

        // Handle cases where the end time is before the start time (overnight)
        if (minuteDifference < 0) {
            minuteDifference += 24 * 60; // Add 24 hours worth of minutes
        }
        return minuteDifference;
    }
    isTimeBetween(startTime, endTime, checkTime) {
        const startDate = new Date(`2000-01-01T${startTime}`);
        const endDate = new Date(`2000-01-01T${endTime}`);
        const checkDate = new Date(`2000-01-01T${checkTime}`);

        if (startDate <= endDate) {
            return startDate <= checkDate && checkDate <= endDate;
        } else {
            return startDate <= checkDate || checkDate <= endDate;
        }
    }

    //===================================================
    //     calculateHourDifference(startTime, endTime) {
    //    // Split the time strings into hours and minutes
    //    const startParts = startTime.split(':');
    //    const endParts = endTime.split(':');

    //    // Convert hours and minutes to integers
    //    const startHour = parseInt(startParts[0], 10);
    //    const startMinute = parseInt(startParts[1], 10);
    //    const endHour = parseInt(endParts[0], 10);
    //    const endMinute = parseInt(endParts[1], 10);

    //    // Calculate the total minutes for start and end times
    //    const totalStartMinutes = (startHour * 60) + startMinute;
    //    const totalEndMinutes = (endHour * 60) + endMinute;

    //    // Calculate the difference in minutes
    //    let minuteDifference = totalEndMinutes - totalStartMinutes;

    //    // Handle cases where the end time is before the start time (overnight)
    //    if (minuteDifference < 0) {
    //        minuteDifference += 24 * 60; // Add 24 hours worth of minutes
    //    }

    //    // Calculate the difference in hours and minutes
    //    const hourDifference = Math.floor(minuteDifference / 60);
    //    const remainingMinutes = minuteDifference % 60;

    //    return { hours: hourDifference, minutes: remainingMinutes };
    //}
    //==============
    //To Convert From 12 To 24 Hour Format
    convertTo24HourFormat(time12h) {

        const [time, period] = time12h.split(' ');
        let [hours, minutes] = time.split(':');

        // Convert hours to integer
        hours = parseInt(hours);
        minutes = parseInt(minutes);

        // Adjust hours according to AM/PM
        if (period === 'PM' && hours < 12) {
            hours += 12;
        } else if (period === 'AM' && hours === 12) {
            hours = 0;
        }

        // Return time in minutes since midnight
        return hours * 60 + minutes;
    }
    convertTo24Hour(time12h) {

        const [time, period] = time12h.split(' ');
        let [hours, minutes] = time.split(':');

        // Convert hours to integer
        hours = parseInt(hours);
        minutes = parseInt(minutes);

        // Adjust hours according to AM/PM
        if (period === 'PM' && hours < 12) {
            hours += 12;
        } else if (period === 'AM' && hours === 12) {
            hours = 0;
        }
        if (minutes == 0) {
            minutes = '00';
        }
        // Return time in minutes since midnight
        return `${hours}:${minutes}`
    }

    //==============
    isStartDateBeforeEndDate(startDate, endDate) {
        return startDate > endDate;
    }
    //==============
    softErrorMessage(message) {
        Swal.fire({
            icon: 'error',
            title: message,
            iconColor: "#d33",
            confirmButtonColor: "#d33",
            confirmButtonText: "حسنا",
        });
    }
    softSuccessMessage(message) {
        Swal.fire({
            icon: 'success',
            title: message,
            iconColor: "#28a745",
            confirmButtonColor: "#28a745",
            confirmButtonText: "حسنا",
        });
    }
    //==============
    hide(selector) {
        const element = document.getElementById(selector);
        element.style.display = 'none';

    }
    //==============
    show(selector) {
        const element = document.getElementById(selector);
        element.style.display = 'block';

    }
    sliceText(text, length) {
        if (text.length > length) {
            return text.slice(0, length);
        } else {
            return text;
        }
    }
    //==============
    //validateTimeRange(fromTime, toTime, startTime, endTime) {
    //    // Convert time strings to Date objects for easier comparison
    //    const fromTimeObj = new Date(`2000-01-01T${fromTime}`);
    //    const toTimeObj = new Date(`2000-01-01T${toTime}`);
    //    const startTimeObj = new Date(`2000-01-01T${startTime}`);
    //    const endTimeObj = new Date(`2000-01-01T${endTime}`);

    //    // Validate if fromTime and toTime are valid times
    //    if (isNaN(fromTimeObj.getTime()) || isNaN(toTimeObj.getTime())) {
    //        return false; // Invalid time format
    //    }

    //    // Validate if startTime and endTime are valid times
    //    if (isNaN(startTimeObj.getTime()) || isNaN(endTimeObj.getTime())) {
    //        return false; // Invalid time format
    //    }

    //    // Check if fromTime is before toTime
    //    if (fromTimeObj >= toTimeObj) {
    //        return false; // fromTime is not before toTime
    //    }

    //    // Check if startTime is before endTime
    //    if (startTimeObj >= endTimeObj) {
    //        return false; // startTime is not before endTime
    //    }

    //    // Check if fromTime is after startTime and before endTime
    //    if (fromTimeObj < startTimeObj || fromTimeObj > endTimeObj) {
    //        return false; // fromTime is not between startTime and endTime
    //    }

    //    // Check if toTime is after startTime and before endTime
    //    if (toTimeObj < startTimeObj || toTimeObj > endTimeObj) {
    //        return false; // toTime is not between startTime and endTime
    //    }

    //    // Both fromTime and toTime are between startTime and endTime
    //    return true;
    //}
    calculateMius(startTime) {

        const startParts = startTime.split(':');
        const startHour = parseInt(startParts[0], 10);
        const startMinute = parseInt(startParts[1], 10);
        const totalStartMinutes = (startHour * 60) + startMinute;
        return totalStartMinutes;

    }
    //getResponse() {
    //    let timerInterval;
    //    Swal.fire({
    //        title: "Auto close alert!",
    //        html: "I will close in <b></b> milliseconds.",
    //        //timer: 2000,
    //        timerProgressBar: true,
    //        didOpen: () => {
    //            Swal.showLoading();
    //            const timer = Swal.getPopup().querySelector("b");
    //            timerInterval = setInterval(() => {
    //                timer.textContent = `${Swal.getTimerLeft()}`;
    //            }, 100);
    //        },
    //        willClose: () => {
    //            clearInterval(timerInterval);
    //        }
    //    }).then((result) => {
    //        /* Read more about handling dismissals below */
    //        if (result.dismiss === Swal.DismissReason.timer) {
    //            console.log("I was closed by the timer");
    //        }
    //    });
    //}
    //=========================


}