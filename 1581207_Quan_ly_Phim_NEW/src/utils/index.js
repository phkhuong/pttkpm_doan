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
    return new Date(parseInt(str.substr(6))).toLocaleString('vi-VN', options);
}