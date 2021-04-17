window.browserResize = {
	getInnerHeight: function (element) {
		if (typeof element != "object") {
			return 0;
		}
		let computed = getComputedStyle(element);
		let padding = parseInt(computed.paddingTop) + parseInt(computed.paddingBottom);

		return element.clientHeight - padding;
	},

	getInnerWidth: function (element) {
		if (typeof element != "object") {
			return 0;
		}
		let computed = getComputedStyle(element);
		let padding = parseInt(computed.paddingLeft) + parseInt(computed.paddingRight);

		return element.clientWidth - padding;
	},

	registerResizeCallback: function () {
		window.removeEventListener("resize", browserResize.resized);
		window.addEventListener("resize", browserResize.resized);
		console.log("window resize event callback registered");
	},

	resized: function () {
		//DotNet.invokeMethod("BrowserResize", 'OnBrowserResize');
		DotNet.invokeMethodAsync("ReplayManager", 'OnBrowserResize').then(data => data);
		console.log("window resize event callback invoked");
	}
}
