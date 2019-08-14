/**
 * Copyright (c) Tiny Technologies, Inc. All rights reserved.
 * Licensed under the LGPL or a commercial license.
 * For LGPL see License.txt in the project root for license information.
 * For commercial licenses see https://www.tiny.cloud/
 *
 * Version: 5.0.1 (2019-02-21)
 */
(function () {
var directionality = (function () {
    'use strict';

    var global = tinymce.util.Tools.resolve('tinymce.PluginManager');

    var global$1 = tinymce.util.Tools.resolve('tinymce.util.Tools');

    var setDir = function (editor, dir) {
      var dom = editor.dom;
      var curDir;
      var blocks = editor.selection.getSelectedBlocks();
      if (blocks.length) {
        curDir = dom.getAttrib(blocks[0], 'dir');
        global$1.each(blocks, function (block) {
          if (!dom.getParent(block.parentNode, '*[dir="' + dir + '"]', dom.getRoot())) {
            dom.setAttrib(block, 'dir', curDir !== dir ? dir : null);
          }
        });
        editor.nodeChanged();
      }
    };
    var Direction = { setDir: setDir };

    var register = function (editor) {
      editor.addCommand('mceDirectionLTR', function () {
        Direction.setDir(editor, 'ltr');
      });
      editor.addCommand('mceDirectionRTL', function () {
        Direction.setDir(editor, 'rtl');
      });
    };
    var Commands = { register: register };

    var generateSelector = function (dir) {
      var selector = [];
      global$1.each('h1 h2 h3 h4 h5 h6 div p'.split(' '), function (name) {
        selector.push(name + '[dir=' + dir + ']');
      });
      return selector.join(',');
    };
    var register$1 = function (editor) {
      editor.ui.registry.addToggleButton('ltr', {
        tooltip: 'Left to right',
        icon: 'ltr',
        onAction: function () {
          return editor.execCommand('mceDirectionLTR');
        },
        onSetup: function (buttonApi) {
          return editor.selection.selectorChangedWithUnbind(generateSelector('ltr'), buttonApi.setActive).unbind;
        }
      });
      editor.ui.registry.addToggleButton('rtl', {
        tooltip: 'Right to left',
        icon: 'rtl',
        onAction: function () {
          return editor.execCommand('mceDirectionRTL');
        },
        onSetup: function (buttonApi) {
          return editor.selection.selectorChangedWithUnbind(generateSelector('rtl'), buttonApi.setActive).unbind;
        }
      });
    };
    var Buttons = { register: register$1 };

    global.add('directionality', function (editor) {
      Commands.register(editor);
      Buttons.register(editor);
    });
    function Plugin () {
    }

    return Plugin;

}());
})();
