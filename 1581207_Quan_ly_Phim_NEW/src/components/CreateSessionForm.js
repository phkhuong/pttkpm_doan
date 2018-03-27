import React from "react";
import { Button, Modal, Form, Input, Radio, DatePicker, Select } from 'antd';
const FormItem = Form.Item;
const Option = Select.Option;

const CollectionCreateForm = Form.create()(
    class extends React.Component {
        render() {
            const { visible, onCancel, onCreate, form } = this.props;
            const { getFieldDecorator } = form;
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
            const dateFormat = "HH:mm, DD/MM/YYYY";
            const rooms = []
            this.props.cinemas.forEach(c => {
                c.Danh_sach_Phong_chieu.forEach((p, i) => rooms.push(<Option key={i} value={p.Ma_so}>{p.Ma_so}</Option>))
            });
            
            return (
                <Modal
                    visible={visible}
                    title="Create a new collection"
                    okText="Create"
                    onCancel={onCancel}
                    onOk={onCreate}
                >
                    <Form layout="vertical">
                        
                        <FormItem
                            {...formItemLayout}
                            label="Thời gian"
                        >
                            {getFieldDecorator(`Bat_dau`, {
                                rules: [{ type: 'object', required: true, message: 'Vui lòng chọn thời gian chiếu!' }],
                                // initialValue: moment()
                            })(
                                <DatePicker format={dateFormat} showTime />
                            )}
                        </FormItem>
                        <FormItem
                            {...formItemLayout}
                            label="Chọn Rạp"
                        >
                            {getFieldDecorator(`Rap.Ma_so`, {
                                rules: [{ required: true, message: 'Vui lòng chọn rạp!' }],
                                // initialValue: Rap ? Rap.Ten : cinemas[0],
                            })(
                                <Select>
                                    {this.props.cinemas.map((c, i) => <Option key={i} value={c.Ma_so}>{c.Ten} (Rạp {c.Ma_so.substr(4)})</Option>)}
                                </Select>
                            )}
                        </FormItem>
                        <FormItem
                            {...formItemLayout}
                            label="Chọn Phòng chiếu"
                        >
                            {getFieldDecorator(`Phong_chieu.Ma_so`, {
                                rules: [{ required: true, message: 'Vui lòng chọn Phòng chiếu!' }],
                                // initialValue: Phong_chieu ? Phong_chieu.Ma_so : cinemas[0],
                            })(
                                <Select>
                                    {rooms}
                                </Select>
                            )}
                        </FormItem>
                        
                    </Form>
                </Modal>
            );
        }
    }
);

export default CollectionCreateForm;