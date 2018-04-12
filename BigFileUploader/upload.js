"use strict";
var uploader = (function () {
    function uploader(file) {
        //this.host = "http://upload.jkbat.com/bigfile/";
        //�ĳ��Լ��ĵ�ַ
        this.host = "http://localhost:37968/Upload/";

        this.running = false;
        this.completed = false;
        this.file = file;
    }
    uploader.prototype.getToken = function () {
        return this.token;
    };
    uploader.prototype.start = function (token) {
        var _this = this;
        if (token === void 0) { token = null; }
        if (this.running || this.completed)
            return;
        if (token != null && token != "")
            this.token = token;
        if (this.token == null) {
            var request = this.createRequest();
            request.onloadend = function () {
                _this.token = JSON.parse(request.responseText).token;
                _this.start();
            };
            request.open("GET", this.host + "?filesize=" + this.file.size);
            request.send();
        }
        else {
            this.running = true;
            this.uploadCore();
        }
    };
    uploader.prototype.stop = function () {
        this.running = false;
    };
    uploader.prototype.createRequest = function () {
        var _this = this;
        var instance = new XMLHttpRequest();
        instance.onerror = this.error;
        instance.ontimeout = this.error;
        instance.onloadend = function () { return _this.uploadCallback(JSON.parse(instance.responseText)); };
        return instance;
    };
    uploader.prototype.error = function (data) {
        console.log(data);
        this.running = false;
        if (this.onerror != null)
            this.onerror(data);
    };
    uploader.prototype.uploadCore = function () {
        var request = this.createRequest();
        request.open("GET", this.host + this.token);
        request.send();
    };
    uploader.prototype.uploadCallback = function (data) {
        if (this.completed)
            return;
        if (this.onprogress != null)
            this.onprogress(data);
        if (this.running == false)
            return;
        var incomplete = data.incomplete;
        if (incomplete == null) {
            if (data.IncompleteBlocks !== 0) {
                this.error(data);
                return;
            }
            else {
                this.complete();
                return;
            }
        }
        this.uploadBlock(incomplete);
    };
    uploader.prototype.uploadBlock = function (incomplete) {
        var block = this.file.slice(incomplete.start, incomplete.end, "application/octet-stream");
        var request = this.createRequest();
        request.open("POST", this.host + this.token + "/" + incomplete.blockIndex);
        request.send(block);
    };
    uploader.prototype.complete = function () {
        var _this = this;
        var request = this.createRequest();
        request.onloadend = function () {
            _this.completeData = JSON.parse(request.responseText);
            _this.completed = true;
            if (_this.oncomplete != null)
                _this.oncomplete(_this.completeData);
        };
        request.open("GET", this.host + this.token + "/?filename=" + this.file.name);
        request.send();
    };
    uploader.prototype.getCompleteData = function () {
        if (this.completed == false)
            return null;
        return this.completeData;
    };
    Object.defineProperty(uploader.prototype, "status", {
        get: function () {
            if (this.completed)
                return "completed";
            else if (this.running)
                return "running";
            else
                return "stopped";
        },
        enumerable: true,
        configurable: true
    });
    return uploader;
}());
exports.uploader = uploader;
//# sourceMappingURL=upload.js.map