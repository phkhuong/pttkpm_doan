export const getDateStringFromJson = (str) => {
    // "\/Date(1518180000000)\/"
    if(!str)
        return "";
    return new Date(parseInt(str.substr(6))).toLocaleDateString('vi-VN');
}
export const getDateTimeStringFromJson = (str) => {
    // "\/Date(1518180000000)\/"
    var options = {
        weekday: 'long', year: 'numeric', month: '2-digit', day: 'numeric', hour: "2-digit", minute:"2-digit" };
    if(!str)
        return "";
    if (Date.parse(str)){
        return new Date(str).toLocaleString('vi-VN', options);
    }
    return new Date(parseInt(str.substr(6))).toLocaleString('vi-VN', options);
}

export function findMaxSessionID(sessions) {
    if (sessions.length == 0)
        return 0;
    let max = sessions.reduce((pre, cur) => {
        if (cur.Ma_so.localeCompare(pre.Ma_so))
            return cur;
    }).Ma_so.substr(2);
    return Number(max);
}
export const defaultSeats = [
    { "Ma_so": 'A1' }, { "Ma_so": 'A2' }, { "Ma_so": 'A3' }, { "Ma_so": 'A4' }, { "Ma_so": 'A5' }, { "Ma_so": 'A6' }, { "Ma_so": 'A7' }, { "Ma_so": 'A8' }, { "Ma_so": 'A9' },
    { "Ma_so": 'B1' }, { "Ma_so": 'B2' }, { "Ma_so": 'B3' }, { "Ma_so": 'B4' }, { "Ma_so": 'B5' }, { "Ma_so": 'B6' }, { "Ma_so": 'B7' }, { "Ma_so": 'B8' }, { "Ma_so": 'B9' },
    { "Ma_so": 'C1' }, { "Ma_so": 'C2' }, { "Ma_so": 'C3' }, { "Ma_so": 'C4' }, { "Ma_so": 'C5' }, { "Ma_so": 'C6' }, { "Ma_so": 'C7' }, { "Ma_so": 'C8' }, { "Ma_so": 'C9' },
    { "Ma_so": 'D1' }, { "Ma_so": 'D2' }, { "Ma_so": 'D3' }, { "Ma_so": 'D4' }, { "Ma_so": 'D5' }, { "Ma_so": 'D6' }, { "Ma_so": 'D7' }, { "Ma_so": 'D8' }, { "Ma_so": 'D9' },
    { "Ma_so": 'E1' }, { "Ma_so": 'E2' }, { "Ma_so": 'E3' }, { "Ma_so": 'E4' }, { "Ma_so": 'E5' }, { "Ma_so": 'E6' }, { "Ma_so": 'E7' }, { "Ma_so": 'E8' }, { "Ma_so": 'E9' },
    { "Ma_so": 'F1' }, { "Ma_so": 'F2' }, { "Ma_so": 'F3' }, { "Ma_so": 'F4' }, { "Ma_so": 'F5' }, { "Ma_so": 'F6' }, { "Ma_so": 'F7' }, { "Ma_so": 'F8' }, { "Ma_so": 'F9' },
    { "Ma_so": 'G1' }, { "Ma_so": 'G2' }, { "Ma_so": 'G3' }, { "Ma_so": 'G4' }, { "Ma_so": 'G5' }, { "Ma_so": 'G6' }, { "Ma_so": 'G7' }, { "Ma_so": 'G8' }, { "Ma_so": 'G9' },
    { "Ma_so": 'H1' }, { "Ma_so": 'H2' }, { "Ma_so": 'H3' }, { "Ma_so": 'H4' }, { "Ma_so": 'H5' }, { "Ma_so": 'H6' }, { "Ma_so": 'H7' }, { "Ma_so": 'H8' }, { "Ma_so": 'H9' },
    { "Ma_so": 'I1' }, { "Ma_so": 'I2' }, { "Ma_so": 'I3' }, { "Ma_so": 'I4' }, { "Ma_so": 'I5' }, { "Ma_so": 'I6' }, { "Ma_so": 'I7' }, { "Ma_so": 'I8' }, { "Ma_so": 'I9' },
]; 