# Watch-Win 

[![NPM](https://nodei.co/npm/watch-win.png)](https://nodei.co/npm/watch-win/)

Native directory tree watcher for [Node.js](http://nodejs.org/) on Windows 

## Installation

	$ npm install watch-win
    
## Usage

```js
var Watcher = require('watch-win');

var watcher = new Watcher({
    path: __dirname    
});

console.log('id: ' + watcher.id()); // native Id

watcher.on('raw', function(data){ // raw event
    console.log(data);
});

watcher.on('changed', function(path){
    console.log('changed: ' + path);    
});

watcher.on('renamed', function(path, oldPath){
    console.log('renamed: ' + oldPath + ' -> ' + path);
});

console.log(watcher.running()); // is running

watcher.close(); // destroy watcher

```

### Events

* *raw* - RAW event emmited by native watcher. Takes `info` argument like this object: 

```js
{ 
  type: 'renamed',
  path: '<path to dir>/test.js',
  name: 'test.js',
  oldPath: '<path to dir>/old/test.js',
  name: 'test.js'
}
```

* *changed* - The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time. Takes `path` argument.
* *created* - The creation of a file or folder. Takes `path` argument.
* *deleted* - The deletion of a file or folder. Takes `path` argument.
* *renamed* - The renaming of a file or folder. Takes `path`, `oldPath` arguments.
* *error* - Native worker emits error. Takes `error` argument


## License
The MIT License

Copyright (c) 2016 Artur Kraev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
