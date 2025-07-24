window.qrScanner = {
    start: function (dotNetRef) {
        if (!window.html5QrCode) {
            window.html5QrCode = new Html5Qrcode("qr-reader");
        }
        window.html5QrCode.start(
            { facingMode: "environment" },
            {
                fps: 10,
                qrbox: 250
            },
            qrCodeMessage => {
                dotNetRef.invokeMethodAsync('OnQrCodeScanned', qrCodeMessage);
                window.html5QrCode.stop();
            },
            errorMessage => {
                // Optionally handle scan errors
            }
        );
    }
};