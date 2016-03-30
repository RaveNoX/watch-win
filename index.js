var edge = require('edge');
var path = require('path');
var EventEmitter = require('events');
var util = require('util');
var _ = require('lodash');

var dllPath = path.resolve(__dirname, 'native', 'WatchWin.dll');
var createClrWatcher = edge.func(dllPath);

var getOptions = function(options) {
  var ret = {
    path: process.cwd(),
    recursive: true,
    filter: null,
    options: {
      fileName: true,
      dirName: true,
      attributes: false,
      size: false,
      lastWrite: true,
      lastAccess: false,
      creationTime: false,
      security: false
    }
  };
  
  _.extend(ret, options);
  
  return ret;
}

function Watcher(options) {
  EventEmitter.call(this);

  var self = this;

  var onWatchCallback = function(data, callback) {
    if (data.type == 'error') {
      self.emit(data.type, data.error);
    }
    else {
      self.emit('raw', data);
      self.emit(data.type, data.path);
    }

    callback();
  };

  var options = getOptions(options);  
  options.callback = onWatchCallback;

  var clrWatcher = createClrWatcher(options, true);
  var closed = false;

  this.id = function() {
    return clrWatcher.id(null, true);
  };

  this.close = function() {
    if (closed) return;

    closed = true;
    clrWatcher.close(null, true);
  };

  this.running = function() {
    return !closed;
  };

  clrWatcher.start();
};

util.inherits(Watcher, EventEmitter);

module.exports = Watcher;