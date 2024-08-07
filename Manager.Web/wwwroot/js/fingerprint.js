//console.log('fingerprint.js loaded');
//window.getFingerprint = async function () {
//    console.log('getFingerprint called');
//    const fp = await FingerprintJS.load();
//    const result = await fp.get();
//    console.log('Fingerprint obtained:', result.visitorId);
//    return result.visitorId;
//};



console.log('fingerprint.js loaded');
window.getFingerprint = async function () {
    console.log('getFingerprint called');
    const fp = await FingerprintJS.load();
    const result = await fp.get();
    console.log('Fingerprint obtained:', result.visitorId);
    return result.visitorId;
};

