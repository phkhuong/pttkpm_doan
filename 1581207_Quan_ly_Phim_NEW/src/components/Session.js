import React from "react";
import { getDateTimeStringFromJson } from "../utils";
import InfoText from "./InfoText";
import InfoLabel from "./InfoLabel";
import AvailableSeats from "./AvailableSeats";

const Session = (props) => {
    const { Bat_dau, Rap, Phong_chieu, Danh_sach_Ghe_trong } = props.session;
    return (
        <div>
            <p><InfoLabel>Thời gian:</InfoLabel> <InfoText>{getDateTimeStringFromJson(Bat_dau)}</InfoText></p>
            <p><InfoLabel>Rạp:</InfoLabel> <InfoText>{Rap.Ten}</InfoText></p>
            <p><InfoLabel>Phòng chiếu:</InfoLabel> <InfoText>{Phong_chieu.Ten}</InfoText></p>
            <AvailableSeats data={Danh_sach_Ghe_trong} />
        </div>
    );
}    

export default Session;