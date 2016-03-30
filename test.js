var Watcher = require('./index.js');

var watcher = new Watcher({
  path: __dirname
});

watcher.on('renamed', function(path, oldPath){
  console.log('rename: ' + oldPath + ' -> ' + path);  
});

watcher.on('changed', function(path){
  console.log('change: ' + path);
});