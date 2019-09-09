let currentEvent;
const formatDate = date => date === null ? '' : moment(date).format("MM/DD/YYYY h:mm A");
const fpStartTime = flatpickr("#StartTime", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});
const fpEndTime = flatpickr("#EndTime", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});

$('#calendar').fullCalendar({
    defaultView: 'month',
    height: 'parent',
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay'
    },
    eventRender(event, $el) {
        $el.qtip({
            content: {
                title: event.title,
                requesterEmail: event.requesterEmail,
                text: event.description,
                filterID: event.filterID,
                sizeID: event.sizeID,
                controllerID: event.controllerID,
                laboratoryId: event.laboratoryId,

                stateId: event.stateId

            },
            hide: {
                event: 'unfocus'
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
    events: '/api/RequestCalendar/GetCalendarEvents',
    eventClick: updateEvent,
    selectable: true,
    select: addEvent
});

/**
 * Calendar Methods
 **/

function updateEvent(event, element) {
    currentEvent = event;

    if ($(this).data("qtip")) $(this).qtip("hide");

    $('#eventModalLabel').html('Edit Event');
    $('#eventModalSave').html('Update Event');
    $('#EventTitle').val(event.title);
    $('#RequesterEmail').val(event.requesterEmail);
    $('#Description').val(event.description);
    $('#SizeID').val(event.sizeID);
    $('#FilterID').val(event.filterID);
    $('#ControllerID').val(event.controllerID);
    $('#LaboratoryId').val(event.laboratoryId);
    $('#StateId').val(event.stateId);
    $('#isNewEvent').val(false);

    const start = formatDate(event.start);
    const end = formatDate(event.end);

    fpStartTime.setDate(start);
    fpEndTime.setDate(end);

    $('#StartTime').val(start);
    $('#EndTime').val(end);

    if (event.allDay) {
        $('#AllDay').prop('checked', 'checked');
    } else {
        $('#AllDay')[0].checked = false;
    }

    $('#eventModal').modal('show');
}

function addEvent(start, end) {
    $('#eventForm')[0].reset();

    $('#eventModalLabel').html('Add Event');
    $('#eventModalSave').html('Create Event');
    $('#isNewEvent').val(true);

    start = formatDate(start);
    end = formatDate(end);

    fpStartTime.setDate(start);
    fpEndTime.setDate(end);

    $('#eventModal').modal('show');
}

/**
 * Modal
 * */

$('#eventModalSave').click(() => {
    const title = $('#EventTitle').val();
    const requesterEmail = $('#RequesterEmail').val();
    const description = $('#Description').val();
    const filterID = $('#FilterID').val();
    const  sizeID = $('#SizeID').val();
   const controllerID = $('#ControllerID').val();
    const laboratoryId = $('#LaboratoryId').val();
    const stateId = $('#StateId').val();

    const isAllDay = $('#AllDay').is(":checked");
    const startTime = moment($('#StartTime').val());
    const endTime = moment($('#EndTime').val());
    const isNewEvent = $('#isNewEvent').val() === 'true' ? true : false;

    if (startTime > endTime) {
        alert('Start Time cannot be greater than End Time');

        return;
    } else if ((!startTime.isValid() || !endTime.isValid()) && !isAllDay) {
        alert('Please enter both Start Time and End Time');

        return;
    }

    const event = {
        title,
        requesterEmail,
        description,
        filterID,
        sizeID,
        controllerID,
        laboratoryId,
        stateId,
        isAllDay,
  
        startTime: startTime._i,
        endTime: endTime._i
    };

    if (isNewEvent) {
        sendAddEvent(event);
    } else {
        sendUpdateEvent(event);
    }
});

function sendAddEvent(event) {
    axios({
        method: 'post',
        url: '/api/RequestCalendar/AddEvent',
        data: {
            "Title": event.title,
            "RequesterEmail": event.requesterEmail,
            "Description": event.description,
            "Start": event.startTime,
            "End": event.endTime,
            "AllDay": event.isAllDay,
            "SizeID": event.filterID,
            "FilterID": event.sizeID,
            "ControllerID": event.controllerID,
            "LaboratoryId": event.laboratoryId,
            "StateId": event.stateId
        }
    })
        .then(res => {
            const { message, eventId } = res.data;

            if (message === '') {
                const newEvent = {
                    start: event.startTime,
                    end: event.endTime,
                    allDay: event.isAllDay,
                    title: event.title,
                    description: event.description,
                    filterID: event.filterID,
                    sizeID: event.sizeID,
                    controllerID: event.controllerID,
                      laboratoryId: event.laboratoryId,
                    requesterEmail: event.requesterEmail,
                    eventId
                };

                $('#calendar').fullCalendar('renderEvent', newEvent);
                $('#calendar').fullCalendar('unselect');

                $('#eventModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

function sendUpdateEvent(event) {
    axios({
        method: 'post',
        url: '/api/RequestCalendar/UpdateEvent',
        data: {
            "EventId": currentEvent.eventId,
            "Title": currentEvent.title,
            "RequesterEmail": event.requesterEmail,
            "Description": event.description,
            "SizeID": (event.sizeID),
            "FilterID": (event.filterID),
            "ControllerID": (event.controllerID),
            "LaboratoryId": (event.laboratoryId),
             "StateId": event.stateId,
            "Start": event.startTime,
            "End": event.endTime,
            "AllDay": event.isAllDay
        }
    })
        .then(res => {
            const { message } = res.data;

            if (message === '') {
                currentEvent.title = event.title;
                currentEvent.description = event.description;
                currentEvent.start = event.startTime;
                currentEvent.end = event.endTime;
                currentEvent.sizeID = event.sizeID;
                currentEvent.filterID = event.filterID;
                currentEvent.controllerID = event.controllerID;
                currentEvent.laboratoryId = event.laboratoryId;
                currentEvent.stateId = event.stateId;
                currentEvent.allDay = event.isAllDay;

                $('#calendar').fullCalendar('updateEvent', currentEvent);
                $('#eventModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

$('#deleteEvent').click(() => {
    if (confirm(`Do you really want to delte "${currentEvent.title}" event?`)) {
        axios({
            method: 'post',
            url: '/api/RequestCalendar/DeleteEvent',
            data: {
                "EventId": currentEvent.eventId
            }
        })
            .then(res => {
                const { message } = res.data;

                if (message === '') {
                    $('#calendar').fullCalendar('removeEvents', currentEvent._id);
                    $('#eventModal').modal('hide');
                } else {
                    alert(`Something went wrong: ${message}`);
                }
            })
            .catch(err => alert(`Something went wrong: ${err}`));
    }
});

$('#AllDay').on('change', function (e) {
    if (e.target.checked) {
        $('#EndTime').val('');
        fpEndTime.clear();
        this.checked = true;
    } else {
        this.checked = false;
    }
});

$('#EndTime').on('change', () => {
    $('#AllDay')[0].checked = false;
});