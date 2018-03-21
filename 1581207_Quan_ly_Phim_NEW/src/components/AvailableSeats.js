import React from "react";

import { List, Tag } from "antd";

const AvailableSeats = (props) => {
    const data = props.data.sort((a, b) => a.Ma_so.localeCompare(b.Ma_so));
    return (
        <List
            header={<h4>Danh sách ghế trống</h4>}
            grid={{ gutter: 16, xs: 3, sm: 5, md: 6, lg: 9  }}
            dataSource={data}
            renderItem={item => (
                <List.Item>
                    <Tag color="#87d068">{item.Ma_so}</Tag>
                </List.Item>
            )}
        />
    );
};
// 
export default AvailableSeats;