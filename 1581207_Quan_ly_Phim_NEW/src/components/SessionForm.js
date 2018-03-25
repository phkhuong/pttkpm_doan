import React from "react";
// import PropTypes from "prop-types";



import { Tree, Collapse, Tabs, Radio, Form, Input, Tooltip, Icon, Cascader, Select, Row, Col, Checkbox, Button, DatePicker } from 'antd';
import * as moment from "moment";
import 'moment/locale/vi';
moment.locale('vi');

const FormItem = Form.Item;
const TabPane = Tabs.TabPane;
const Panel = Collapse.Panel;
const Option = Select.Option;
const TreeNode = Tree.TreeNode;

class SessionForm extends React.Component {
    
    

    handleDeleteSession(sessionID){
        this.props.deleteSession(this.props.movieID, sessionID);
    }
    
    render() {
        const formItemLayout = {
            labelCol: {
                xs: { span: 24 },
                sm: { span: 8 },
            },
            wrapperCol: {
                xs: { span: 24 },
                sm: { span: 16 },
            },
        };
        const getFieldDecorator = this.props.getFieldDecorator;
        const getFieldValue = this.props.getFieldValue;
        const tailFormItemLayout = this.props.tailFormItemLayout;

        const cinemas = [];
        const rooms = []
        this.props.cinemas.forEach(c => {
            
            c.Danh_sach_Phong_chieu.forEach((p, i) => rooms.push(<Option key={i} value={p.Ma_so}>{p.Ma_so}</Option>))
        });
        // console.log(rooms);
        
        const checkboxOptions = [
            'A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'A9',
            'B1', 'B2', 'B3', 'B4', 'B5', 'B6', 'B7', 'B8', 'B9',
            'C1', 'C2', 'C3', 'C4', 'C5', 'C6', 'C7', 'C8', 'C9',
            'D1', 'D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D8', 'D9',
            'E1', 'E2', 'E3', 'E4', 'E5', 'E6', 'E7', 'E8', 'E9',
            'F1', 'F2', 'F3', 'F4', 'F5', 'F6', 'F7', 'F8', 'F9',
            'G1', 'G2', 'G3', 'G4', 'G5', 'G6', 'G7', 'G8', 'G9',
            'H1', 'H2', 'H3', 'H4', 'H5', 'H6', 'H7', 'H8', 'H9',
            'I1', 'I2', 'I3', 'I4', 'I5', 'I6', 'I7', 'I8', 'I9',
        ]; 

        const list = this.props.sessions.map((s => {
            const { Ma_so, Bat_dau, Rap, Phong_chieu, Danh_sach_Ghe_trong } = s;
            const dateFormat = "HH:mm, DD/MM/YYYY";
            const checkboxDefaultOptions = Danh_sach_Ghe_trong.map(g => {
                return g.Ma_so;
            });

            return (
                <Panel header={Ma_so} key={Ma_so}>
                    <FormItem
                    >
                        {getFieldDecorator(`${Ma_so}.Ma_so`, {
                            
                            initialValue: Ma_so
                        })(
                            <Input hidden />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Thời gian"
                    >
                        {getFieldDecorator(`${Ma_so}.Bat_dau`, {
                            rules: [{ type: 'object', required: true, message: 'Vui lòng chọn thời gian chiếu!' }],
                            initialValue: Bat_dau ? moment(Bat_dau) : moment()
                        })(
                            <DatePicker format={dateFormat} showTime />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Chọn Rạp"
                    >
                        {getFieldDecorator(`${Ma_so}.Rap.Ma_so`, {
                            rules: [{ required: true, message: 'Vui lòng chọn rạp!' }],
                            initialValue: Rap ? Rap.Ma_so : cinemas[0],
                        })(
                            <Select>
                                {this.props.cinemas.map((c,i) => <Option key={i} value={c.Ma_so}>{c.Ten} (Rạp {c.Ma_so.substr(4)})</Option>)}
                            </Select>
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Chọn Phòng chiếu"
                    >
                        {getFieldDecorator(`${Ma_so}.Phong_chieu.Ma_so`, {
                            rules: [{ required: true, message: 'Vui lòng chọn Phòng chiếu!' }],
                            initialValue: Phong_chieu ? Phong_chieu.Ma_so : cinemas[0],
                        })(
                            <Select
                                onChange={this.handleSelectChange}
                            >
                                { rooms }
                            </Select>
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Chọn Ghế trống"
                    >
                        {getFieldDecorator(`${Ma_so}.Danh_sach_Ghe_trong`, {
                           // rules: [{ required: true, message: 'Vui lòng chọn Ghế trống!' }],
                            initialValue: checkboxDefaultOptions,
                        })(
                            <Checkbox.Group style={{ width: '100%', fontSize: 16 }} options={checkboxOptions} >
                                
                            </Checkbox.Group>,
                        )}
                    </FormItem>
                    
                    
                    
                    <FormItem {...tailFormItemLayout}>
                        <Button onClick={() => this.handleDeleteSession(Ma_so)} type="danger">Xóa Suất chiếu</Button>
                    </FormItem>
                </Panel>
            );
        }));
        return (
            <div>
                
                <Collapse
                    tabPosition="left"

                >
                    {list}
                </Collapse>
            </div>
        );
    }
}


export default SessionForm;