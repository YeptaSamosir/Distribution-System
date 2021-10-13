setTimeout(function () {

    const timeElapsed = Date.now();
    const today = new Date(timeElapsed);
    today.toDateString();
    $('#dateToday').text(today.toDateString());

    $.ajax({
        url: `/admin/dashboard/data-count`,
    }).done((result) => {
        console.log(result);


        $('#totalCandidate').text(`${result.totalCandidate}`);
        $('#totalCompany').text(`${result.totalCompany}`);
        $('#totalCandidateIdle').text(`${result.totalCandidateIdle}`);
        $('#totalCandidateOnboard').text(`${result.totalCandidateOnboard}`);
        $('#totalInterviewOngoing').text(`${result.totalInterviewOngoing}`);
        $('#totalInterviewSuccess').text(`${result.totalInterviewSuccess}`);
        $('#totalInterviewCanceled').text(`${result.totalInterviewCanceled}`);
        $('#totalOnboard').text(`${result.totalOnboard}`);

    }).fail((result) => {
        console.log(result);
    });
}, 1000);


/* CALENDAR */

function init_calendar() {

    if (typeof ($.fn.fullCalendar) === 'undefined') { return; }
    console.log('init_calendar');

    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listMonth'
        },
        selectable: true,
        selectHelper: true,
        eventClick: function (calEvent, jsEvent, view) {
            $('#fc_detail').click();
            $('#interviewId').text(calEvent.data.scheduleInterviewId);
            $('#candidateName').text(calEvent.data.candidate.name);
            $('#candidateEmail').text(calEvent.data.detailScheduleInterviews[0].emailCandidate);
            $('#customerName').text(calEvent.data.customerName);
            $('#customerEmail').text(calEvent.data.detailScheduleInterviews[0].emailCustomer);
            $('#companyName').text(calEvent.data.company.name);
            $('#jobTitle').text(calEvent.data.jobTitle);
            $('#type').text(calEvent.data.detailScheduleInterviews[0].typeLocation);
            $('#startDate').text(calEvent.start);
            calendar.fullCalendar('unselect');
        },
        editable: true,
        events: "/admin/dashboard/data-for-calender"
    });

};
