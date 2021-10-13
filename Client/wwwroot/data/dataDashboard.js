setTimeout(function () {
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