/*<div id = "calendar-wrapper" >
    < div id='calendar'></div>
</div>

<div class="modal fade" id="requestModal" role="dialog" aria-labelledby="requestModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="requestModalLabel">Request</h4>
                <button type = "button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                <form id = "requestForm" >
                    < div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Requester Email</label>
                        <div class="col-sm-9">
                            <input type = "text" class="form-control" id="RequesterEmail">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Start Time</label>
                        <div class="col-sm-9">
                            <input type = "text" class="form-control" id="Start">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">End Time</label>
                        <div class="col-sm-9">
                            <input type = "text" class="form-control" id="End">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Filter Type</label>
                        <div class="col-sm-9">
                            <input type = "text" class="dropdown" id="FilterID">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Filter Size</label>
                        <div class="col-sm-9">
                            <input type = "text" class="dropdown" id="SizeID">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Controller Type</label>
                        <div class="col-sm-9">
                            <input type = "text" class="dropdown" id="ControllerID">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Laboratory Name</label>
                        <div class="col-sm-9">
                            <input type = "text" class="dropdown" id="LaboratoryId">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">Description</label>
                        <div class="col-sm-9">
                            <textarea class="form-control" id="Description" rows="5"></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="" class="col-sm-3 col-form-label">State</label>
                        <div class="col-sm-9">
                            <input type = "text" contenteditable="false" id="StateId" />
                        </div>
                    </div>

                    <input type = "hidden" id="isNewRequest" />
                </form>
            </div>
            <div class="modal-footer">
                <button type = "button" class="btn btn-danger" id="deleteRequest">Delete</button>
                <div>
                    <button type = "button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type = "button" class="btn btn-primary" id="requestModalSave">Save Changes</button>
                  </div>
            </div>
        </div>
    </div>
</div>
*/