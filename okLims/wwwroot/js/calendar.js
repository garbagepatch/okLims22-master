// modules are defined as an array
// [ module function, map of requires ]
//
// map of requires is short require name -> numeric require
//
// anything defined in a previous bundle is accessed via the
// orig method which is the require for previous bundles
parcelRequire = (function (modules, cache, entry, globalName) {
  // Save the require from previous bundle to this closure if any
  var previousRequire = typeof parcelRequire === 'function' && parcelRequire;
  var nodeRequire = typeof require === 'function' && require;

  function newRequire(name, jumped) {
    if (!cache[name]) {
      if (!modules[name]) {
        // if we cannot find the module within our internal map or
        // cache jump to the current global require ie. the last bundle
        // that was added to the page.
        var currentRequire = typeof parcelRequire === 'function' && parcelRequire;
        if (!jumped && currentRequire) {
          return currentRequire(name, true);
        }

        // If there are other bundles on this page the require from the
        // previous one is saved to 'previousRequire'. Repeat this as
        // many times as there are bundles until the module is found or
        // we exhaust the require chain.
        if (previousRequire) {
          return previousRequire(name, true);
        }

        // Try the node require function if it exists.
        if (nodeRequire && typeof name === 'string') {
          return nodeRequire(name);
        }

        var err = new Error('Cannot find module \'' + name + '\'');
        err.code = 'MODULE_NOT_FOUND';
        throw err;
      }

      localRequire.resolve = resolve;
      localRequire.cache = {};

      var module = cache[name] = new newRequire.Module(name);

      modules[name][0].call(module.exports, localRequire, module, module.exports, this);
    }

    return cache[name].exports;

    function localRequire(x){
      return newRequire(localRequire.resolve(x));
    }

    function resolve(x){
      return modules[name][1][x] || x;
    }
  }

  function Module(moduleName) {
    this.id = moduleName;
    this.bundle = newRequire;
    this.exports = {};
  }

  newRequire.isParcelRequire = true;
  newRequire.Module = Module;
  newRequire.modules = modules;
  newRequire.cache = cache;
  newRequire.parent = previousRequire;
  newRequire.register = function (id, exports) {
    modules[id] = [function (require, module) {
      module.exports = exports;
    }, {}];
  };

  var error;
  for (var i = 0; i < entry.length; i++) {
    try {
      newRequire(entry[i]);
    } catch (e) {
      // Save first error but execute all entries
      if (!error) {
        error = e;
      }
    }
  }

  if (entry.length) {
    // Expose entry point to Node, AMD or browser globals
    // Based on https://github.com/ForbesLindesay/umd/blob/master/template.js
    var mainExports = newRequire(entry[entry.length - 1]);

    // CommonJS
    if (typeof exports === "object" && typeof module !== "undefined") {
      module.exports = mainExports;

    // RequireJS
    } else if (typeof define === "function" && define.amd) {
     define(function () {
       return mainExports;
     });

    // <script>
    } else if (globalName) {
      this[globalName] = mainExports;
    }
  }

  // Override the current require with this new one
  parcelRequire = newRequire;

  if (error) {
    // throw error from earlier, _after updating parcelRequire_
    throw error;
  }

  return newRequire;
})({"calendar.js":[function(require,module,exports) {
var currentRequest;

var formatDate = function formatDate(date) {
  return date === null ? '' : moment(date).format("MM/DD/YYYY h:mm A");
};

var fpStart = flatpickr("#Start", {
  enableTime: true,
  dateFormat: "m/d/Y h:i K"
});
var fpEnd = flatpickr("#End", {
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
  requestRender: function requestRender(request, $el) {
    $el.qtip({
      content: {
        requesterEmail: request.RequesterEmail,
        specialDetails: request.SpecialDetails,
        filterID: request.FilterID,
        sizeID: request.SizeID,
        controllerID: request.ControllerID,
        laboratoryId: request.LaboratoryId,
        stateId: request.StateId
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
  var start = formatDate(request.Start);
  var end = formatDate(request.End);
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


$('#requestModalSave').click(function () {
  var requesterEmail = $('#RequesterEmail').val();
  var specialDetails = $('#SpecialDetails').val();
  var start = moment($('#Start').val());
  var end = moment($('#End').val());
  var filterID = $('#FilterID').val();
  var sizeID = $('#SizeID').val();
  var controllerID = $('#ControllerID').val();
  var laboratoryId = $('#LaboratoryId').val();
  var stateId = $('#StateId').val();
  var isNewRequest = $('#isNewRequest').val() === 'true' ? true : false;

  if (start > end) {
    alert('Start Time cannot be greater than End Time');
    return;
  } else if (!start.isValid() || !end.isValid()) {
    alert('Please enter both Start Time and End Time');
    return;
  }

  var request = {
    requesterEmail: requesterEmail,
    specialDetails: specialDetails,
    filterID: filterID,
    sizeID: sizeID,
    controllerID: controllerID,
    laboratoryId: laboratoryId,
    stateId: stateId,
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
      "State": request.StateId
    }
  }).then(function (res) {
    var _res$data = res.data,
        message = _res$data.message,
        RequestId = _res$data.RequestId;

    if (message === '') {
      var newRequest = {
        start: request.Start,
        end: request.End,
        requesterEmail: request.RequesterEmail,
        specialDetails: request.SpecialDetails,
        filterID: request.FilterID,
        sizeID: request.SizeID,
        controllerID: request.ControllerID,
        laboratoryId: request.LaboratoryId,
        RequestId: RequestId
      };
      $('#calendar').fullCalendar('renderRequest', newRequest);
      $('#calendar').fullCalendar('unselect');
      $('#requestModal').modal('hide');
    } else {
      alert("Something went wrong: ".concat(message));
    }
  }).catch(function (err) {
    return alert("Something went wrong: ".concat(err));
  });
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
      "LaboratoryName": request.LaboratoryId
    }
  }).then(function (res) {
    var message = res.data.message;

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
      alert("Something went wrong: ".concat(message));
    }
  }).catch(function (err) {
    return alert("Something went wrong: ".concat(err));
  });
}

$('#deleteRequest').click(function () {
  if (confirm("Do you really want to delte \"".concat(currentRequest.requesterEmail, "\" request?"))) {
    axios({
      method: 'post',
      url: '/api/RequestCalendar/DeleteRequest',
      data: {
        "RequestId": currentRequest.RequestId
      }
    }).then(function (res) {
      var message = res.data.message;

      if (message === '') {
        $('#calendar').fullCalendar('removeRequests', currentRequest._id);
        $('#requestModal').modal('hide');
      } else {
        alert("Something went wrong: ".concat(message));
      }
    }).catch(function (err) {
      return alert("Something went wrong: ".concat(err));
    });
  }
});
},{}],"../../../../../AppData/Roaming/npm/node_modules/parcel-bundler/src/builtins/hmr-runtime.js":[function(require,module,exports) {
var global = arguments[3];
var OVERLAY_ID = '__parcel__error__overlay__';
var OldModule = module.bundle.Module;

function Module(moduleName) {
  OldModule.call(this, moduleName);
  this.hot = {
    data: module.bundle.hotData,
    _acceptCallbacks: [],
    _disposeCallbacks: [],
    accept: function (fn) {
      this._acceptCallbacks.push(fn || function () {});
    },
    dispose: function (fn) {
      this._disposeCallbacks.push(fn);
    }
  };
  module.bundle.hotData = null;
}

module.bundle.Module = Module;
var checkedAssets, assetsToAccept;
var parent = module.bundle.parent;

if ((!parent || !parent.isParcelRequire) && typeof WebSocket !== 'undefined') {
  var hostname = "" || location.hostname;
  var protocol = location.protocol === 'https:' ? 'wss' : 'ws';
  var ws = new WebSocket(protocol + '://' + hostname + ':' + "51552" + '/');

  ws.onmessage = function (event) {
    checkedAssets = {};
    assetsToAccept = [];
    var data = JSON.parse(event.data);

    if (data.type === 'update') {
      var handled = false;
      data.assets.forEach(function (asset) {
        if (!asset.isNew) {
          var didAccept = hmrAcceptCheck(global.parcelRequire, asset.id);

          if (didAccept) {
            handled = true;
          }
        }
      }); // Enable HMR for CSS by default.

      handled = handled || data.assets.every(function (asset) {
        return asset.type === 'css' && asset.generated.js;
      });

      if (handled) {
        console.clear();
        data.assets.forEach(function (asset) {
          hmrApply(global.parcelRequire, asset);
        });
        assetsToAccept.forEach(function (v) {
          hmrAcceptRun(v[0], v[1]);
        });
      } else {
        window.location.reload();
      }
    }

    if (data.type === 'reload') {
      ws.close();

      ws.onclose = function () {
        location.reload();
      };
    }

    if (data.type === 'error-resolved') {
      console.log('[parcel] ✨ Error resolved');
      removeErrorOverlay();
    }

    if (data.type === 'error') {
      console.error('[parcel] 🚨  ' + data.error.message + '\n' + data.error.stack);
      removeErrorOverlay();
      var overlay = createErrorOverlay(data);
      document.body.appendChild(overlay);
    }
  };
}

function removeErrorOverlay() {
  var overlay = document.getElementById(OVERLAY_ID);

  if (overlay) {
    overlay.remove();
  }
}

function createErrorOverlay(data) {
  var overlay = document.createElement('div');
  overlay.id = OVERLAY_ID; // html encode message and stack trace

  var message = document.createElement('div');
  var stackTrace = document.createElement('pre');
  message.innerText = data.error.message;
  stackTrace.innerText = data.error.stack;
  overlay.innerHTML = '<div style="background: black; font-size: 16px; color: white; position: fixed; height: 100%; width: 100%; top: 0px; left: 0px; padding: 30px; opacity: 0.85; font-family: Menlo, Consolas, monospace; z-index: 9999;">' + '<span style="background: red; padding: 2px 4px; border-radius: 2px;">ERROR</span>' + '<span style="top: 2px; margin-left: 5px; position: relative;">🚨</span>' + '<div style="font-size: 18px; font-weight: bold; margin-top: 20px;">' + message.innerHTML + '</div>' + '<pre>' + stackTrace.innerHTML + '</pre>' + '</div>';
  return overlay;
}

function getParents(bundle, id) {
  var modules = bundle.modules;

  if (!modules) {
    return [];
  }

  var parents = [];
  var k, d, dep;

  for (k in modules) {
    for (d in modules[k][1]) {
      dep = modules[k][1][d];

      if (dep === id || Array.isArray(dep) && dep[dep.length - 1] === id) {
        parents.push(k);
      }
    }
  }

  if (bundle.parent) {
    parents = parents.concat(getParents(bundle.parent, id));
  }

  return parents;
}

function hmrApply(bundle, asset) {
  var modules = bundle.modules;

  if (!modules) {
    return;
  }

  if (modules[asset.id] || !bundle.parent) {
    var fn = new Function('require', 'module', 'exports', asset.generated.js);
    asset.isNew = !modules[asset.id];
    modules[asset.id] = [fn, asset.deps];
  } else if (bundle.parent) {
    hmrApply(bundle.parent, asset);
  }
}

function hmrAcceptCheck(bundle, id) {
  var modules = bundle.modules;

  if (!modules) {
    return;
  }

  if (!modules[id] && bundle.parent) {
    return hmrAcceptCheck(bundle.parent, id);
  }

  if (checkedAssets[id]) {
    return;
  }

  checkedAssets[id] = true;
  var cached = bundle.cache[id];
  assetsToAccept.push([bundle, id]);

  if (cached && cached.hot && cached.hot._acceptCallbacks.length) {
    return true;
  }

  return getParents(global.parcelRequire, id).some(function (id) {
    return hmrAcceptCheck(global.parcelRequire, id);
  });
}

function hmrAcceptRun(bundle, id) {
  var cached = bundle.cache[id];
  bundle.hotData = {};

  if (cached) {
    cached.hot.data = bundle.hotData;
  }

  if (cached && cached.hot && cached.hot._disposeCallbacks.length) {
    cached.hot._disposeCallbacks.forEach(function (cb) {
      cb(bundle.hotData);
    });
  }

  delete bundle.cache[id];
  bundle(id);
  cached = bundle.cache[id];

  if (cached && cached.hot && cached.hot._acceptCallbacks.length) {
    cached.hot._acceptCallbacks.forEach(function (cb) {
      cb();
    });

    return true;
  }
}
},{}]},{},["../../../../../AppData/Roaming/npm/node_modules/parcel-bundler/src/builtins/hmr-runtime.js","calendar.js"], null)