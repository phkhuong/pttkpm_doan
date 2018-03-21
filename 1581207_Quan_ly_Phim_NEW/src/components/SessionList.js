import React from "react";
import PropTypes from "prop-types";

import Session from "./Session";

import { Tabs, Radio } from 'antd';
const TabPane = Tabs.TabPane;

class SessionList extends React.Component {
    
    render() {
        const list = this.props.sessions.map((s => {
            return (
                <TabPane tab={s.Ma_so} key={s.Ma_so}>
                    <Session key={s.Ma_so} session={s} />
                </TabPane>    
                );
        }));
        return (
            <div>
                
                <Tabs
                    defaultActiveKey={this.props.sessions.length !== 0 && this.props.sessions[0].Ma_so}
                    tabPosition="left"
                   
                >
                    {list}
                </Tabs>
            </div>
        );
    }
}


export default SessionList;