'use strict';

Object.defineProperty(exports, '__esModule', { value: true });

var randomUUID = typeof crypto !== 'undefined' && crypto.randomUUID && crypto.randomUUID.bind(crypto);
var native = {
  randomUUID
};

exports.default = native;
