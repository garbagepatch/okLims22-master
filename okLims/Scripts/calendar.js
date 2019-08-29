let currentRequest;
const formatDate = date => date === null ? '' : moment(date).format("MM/DD/YYYY h:mm A");
const fpStart = flatpickr("#Start", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});
const fpEnd = flatpickr("#End", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});

$('#calendar').fullCalendar({
    defaultView: 'month',
    height: 'parent',
    header: {
        left: 'prev,next today',
        center: 'requesterEmail',
        right: 'month,agendaWeek,agendaDay'
    },
    requestRender(request, $el) {
        $el.qtip({
            content: {
                requesterEmail: request.RequesterEmail,
                specialDetails: request.SpecialDetails,
                filterID: request.FilterID,
                sizeID: request.SizeID,
                controllerID: request.ControllerID,
                laboratoryId: request.LaboratoryId,
                stateId: request.StateId,
            },
            hide: {
                request: 'unfocus'
            },
            show: {
                solo: true
            },
            position: {
                my: 'top left',
                at: 'bottom left',
                viewport: $('#calendar-wrapper'),
                adjust: {
                    method: 'shift'
                }
            }
        });
    },
    requests: 'api/RequestCalendar/GetCalendarRequests',
    requestClick: updateRequest,
    selectable: true,
    select: addRequest
});

/**
 * Calendar Methods
 **/

function updateRequest(request, element) {
    currentRequest = request;

    if ($(this).data("qtip")) $(this).qtip("hide");

    $('#requestModalLabel').html('Edit Request');
    $('#requestModalSave').html('Update Request');
    $('#RequestRequesterEmail').val(request.RequesterEmail);
    $('#SpecialDetails').val(request.SpecialDetails);
    $('#FilterType').val(request.FilterID);
    $('#FilterSize').val(request.SizeID);
    $('#ControllerType').val(request.ControllerID);
    $('#LaboratoryName').val(request.LaboratoryId);
    $('#State').val(request.StateId);
    $('#isNewRequest').val(false);

    const start = formatDate(request.Start);
    const end = formatDate(request.End);

    fpStart.setDate(start);
    fpEnd.setDate(end);

    $('#Start').val(start);
    $('#End').val(end);

 

    $('#requestModal').modal('show');
}

function addRequest(start, end) {
    $('#requestForm')[0].reset();

    $('#requestModalLabel').html('Add Request');
    $('#requestModalSave').html('Create Request');
    $('#isNewRequest').val(true);

    start = formatDate(start);
    end = formatDate(end);

    fpStart.setDate(start);
    fpEnd.setDate(end);

    $('#requestModal').modal('show');
}

/**
 * Modal
 * */

$('#requestModalSave').click(() => {
    const requesterEmail = $('#RequesterEmail').val();
    const specialDetails = $('#SpecialDetails').val();
    const start = moment($('#Start').val());
    const end = moment($('#End').val());
    const filterID = $('#FilterID').val();
    const sizeID = $('#SizeID').val();
    const controllerID = $('#ControllerID').val();
    const laboratoryId = $('#LaboratoryId').val();
    const stateId = $('#StateId').val();

    const isNewRequest = $('#isNewRequest').val() === 'true' ? true : false;

    if (start > end) {
        alert('Start Time cannot be greater than End Time');

        return;
    } else if ((!start.isValid() || !end.isValid()) ) {
        alert('Please enter both Start Time and End Time');

        return;
    }

    const request = {
        requesterEmail,
        specialDetails,
        filterID,
        sizeID,
        controllerID,
        laboratoryId,
        stateId,
        start: start._i,
        end: end._i
    };

    if (isNewRequest) {
        sendAddRequest(request);
    } else {
        sendUpdateRequest(request);
    }
});

function sendAddRequest(request) {
    axios({
        method: 'post',
        url: '/api/RequestCalendar/AddRequest',
        data: {
            "RequesterEmail": request.RequesterEmail,
            "SpecialDetails": request.SpecialDetails,
            "Start": request.Start,
            "End": request.End,
            "FilterType": request.FilterID,
            "FilterSize": request.SizeID,
            "ControllerType": request.ControllerID,
            "LaboratoryName": request.LaboratoryId,
            "State": request.StateId,

        }
    })
        .then(res => {
            const { message, RequestId } = res.data;

            if (message === '') {
                const newRequest = {
                    start: request.Start,
                    end: request.End,                 
                    requesterEmail: request.RequesterEmail,
                    specialDetails: request.SpecialDetails,
                    filterID: request.FilterID,
                    sizeID: request.SizeID,
                    controllerID: request.ControllerID,
                    laboratoryId: request.LaboratoryId,
                    RequestId
                };

                $('#calendar').fullCalendar('renderRequest', newRequest);
                $('#calendar').fullCalendar('unselect');

                $('#requestModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

function sendUpdateRequest(request) {
    axios({
        method: 'post',
        url: '/api/RequestCalendar/UpdateRequest',
        data: {
            "RequestId": currentRequest.RequestId,
            "RequesterEmail": request.RequesterEmail,
            "SpecialDetails": request.SpecialDetails,
            "Start": request.Start,
            "End": request.End,
            "FilterType": request.FilterID,
            "FilterSize": request.SizeID,
            "ControllerType": request.ControllerID,
            "LaboratoryName": request.LaboratoryId,
  
        }
    })
        .then(res => {
            const { message } = res.data;

            if (message === '') {
                currentRequest.requesterEmail = request.RequesterEmail;
                currentRequest.specialDetails = request.SpecialDetails;
                currentRequest.start = request.Start;
                currentRequest.end = request.End;
                currentRequest.filterID = request.FilterID;
                currentRequest.sizeID = request.SizeID;
                currentRequest.controllerID = request.ControllerID;
                currentRequest.laboratoryId = request.LaboratoryId;
                currentRequest.stateId = request.StateId;

                $('#calendar').fullCalendar('updateRequest', currentRequest);
                $('#requestModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

$('#deleteRequest').click(() => {
    if (confirm(`Do you really want to delte "${currentRequest.requesterEmail}" request?`)) {
        axios({
            method: 'post',
            url: '/api/RequestCalendar/DeleteRequest',
            data: {
                "RequestId": currentRequest.RequestId
            }
        })
            .then(res => {
                const { message } = res.data;

                if (message === '') {
                    $('#calendar').fullCalendar('removeRequests', currentRequest._id);
                    $('#requestModal').modal('hide');
                } else {
                    alert(`Something went wrong: ${message}`);
                }
            })
            .catch(err => alert(`Something went wrong: ${err}`));
    }
});


