import React from "react";
import * as moment from 'moment';
import { defaultSeats, findMaxSessionID } from "../utils";
import SessionForm from "./SessionForm";
import CollectionCreateForm from "./CreateSessionForm";
import { message, InputNumber, Form, Input, Tooltip, Icon, Cascader, Select, Row, Col, Checkbox, Button, AutoComplete, DatePicker, Alert } from 'antd';
const FormItem = Form.Item;
const Option = Select.Option;
const AutoCompleteOption = AutoComplete.Option;
const { TextArea } = Input;



class MyForm extends React.Component {
    state = {
        confirmDirty: false,
        visible: false,
    };
    onSuccess(mess) {
        message.success(mess);
        this.props.resetApiResult();
    }
    onFail(mess) {
        message.error(mess);
        this.props.resetApiResult();
    }
    handleSubmit = (e) => {
        e.preventDefault();
        this.props.form.validateFieldsAndScroll((err, fieldsValue) => {
            if (err) {
                return;
            }

            // Tao danh sach Suat chieu
            let key = "S_";
            let ds_suat_chieu = [];
            for (var i = 1; i < Object.keys(fieldsValue).length; i++) {
                let k = key + i; // "S_1"
                if (!fieldsValue[k])
                    continue;

                fieldsValue[k].Bat_dau = fieldsValue[k].Bat_dau.format();
                ds_suat_chieu.push(fieldsValue[k]);
                delete fieldsValue[k];
            }
            ds_suat_chieu.forEach(sc => {
                let ds_ghe = sc.Danh_sach_Ghe_trong.map(g => {
                    return {
                        Ma_so: g
                    };
                });
                sc.Danh_sach_Ghe_trong = ds_ghe;

                let rap = this.props.cinemas.find(c => c.Ma_so == sc.Rap.Ma_so)
                sc.Rap.Ten = rap.Ten;
                // sc.Phong_chieu.Ten = rap.Danh_sach_Phong_chieu.find(p => p.Ma_so == sc.Phong_chieu.Ma_so).Ten;
            });

            // Tao Json kq
            const values = {
                ...fieldsValue,
                'Khoi_chieu': fieldsValue['Khoi_chieu'].format('YYYY-MM-DD'),
                'Danh_sach_Suat_chieu': ds_suat_chieu,
                'The_loai': fieldsValue['The_loai'].split(',').map(s => s.trim())
            }
            console.log('Received values of form: ', values);
            // action -> api
            if(this.props.requestAddMovie)
                this.props.requestAddMovie(values);
            else if (this.props.requestUpdateMovie)
                this.props.requestUpdateMovie(values);
        });
    }
    handleConfirmBlur = (e) => {
        const value = e.target.value;
        this.setState({ confirmDirty: this.state.confirmDirty || !!value });
    }
    showModal = () => {
        this.setState({ visible: true });
    }
    handleCancel = () => {
        this.setState({ visible: false });
    }

    // form in modal: dispatch CREATE_SESSION
    handleCreate = () => {
        const form = this.formRef.props.form;
        form.validateFields((err, values) => {
            if (err) {
                return;
            }

            let newSession = {
                ...values,
                "Bat_dau": values["Bat_dau"].format(),
            };
            newSession.Rap.Ten = this.props.cinemas.find(c => c.Ma_so == newSession.Rap.Ma_so).Ten;
            newSession.Danh_sach_Ghe_trong = defaultSeats;
            newSession.Ma_so = 'S_' + (findMaxSessionID(this.props.Danh_sach_Suat_chieu.value) + 1);
            // console.log('Received values of form: ', newSession);

            // them vao state: onfieldchange
            this.props.onChange({"Danh_sach_Suat_chieu": {value: this.props.Danh_sach_Suat_chieu.value.concat(newSession)}});

            form.resetFields();
            this.setState({ visible: false });
        });
    }
    saveFormRef = (formRef) => {
        this.formRef = formRef;
    }
    render() {
        const { getFieldDecorator, getFieldValue } = this.props.form;
        const { autoCompleteResult } = this.state;

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
        const tailFormItemLayout = {
            wrapperCol: {
                xs: {
                    span: 24,
                    offset: 0,
                },
                sm: {
                    span: 16,
                    offset: 8,
                },
            },
        };

        const dateFormat = 'DD/MM/YYYY';
        var btn
        if(this.props.requestUpdateMovie)
            btn = "Cập nhật Phim";
        else
            btn = "Thêm Phim mới";

        // movie detail
        if (this.props.movie) {
            var { Ma_so, Ten, Noi_dung, Rating, Thoi_luong, Quoc_gia, The_loai, Dien_vien, Dao_dien, Nha_san_xuat, Khoi_chieu, Don_gia, Phan_loai, Doanh_thu, Danh_sach_Suat_chieu } = this.props.movie;
            // if (Khoi_chieu != undefined) {
            //     Khoi_chieu = new Date(parseInt(Khoi_chieu.substr(6))).toLocaleDateString('vi-VN');
            // }
            // if (The_loai != undefined) {
            //     The_loai = The_loai.join(', ');
            // }
        } else {
            var Ma_so, Ten, Noi_dung, Rating, Thoi_luong, Quoc_gia, The_loai, Dien_vien, Dao_dien, Nha_san_xuat, Khoi_chieu, Don_gia, Phan_loai, Doanh_thu, Danh_sach_Suat_chieu;
        }
        return (
            <div>
                <Form onSubmit={this.handleSubmit}>
                    <FormItem
                    >
                        {getFieldDecorator("Ma_so", {

                            initialValue: Ma_so
                        })(
                            <Input hidden disabled />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Tên phim"
                    >
                        {getFieldDecorator('Ten', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập tên phim!',
                            }],
                            initialValue: Ten
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Rating"
                    >
                        {getFieldDecorator('Rating', {
                            rules: [{
                                type: 'number', message: 'Rating phải là số!',
                            },
                            {
                                required: true, message: 'Vui lòng nhập Rating!',
                            }],
                            initialValue: Rating
                        })(
                            <InputNumber />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Phân loại"
                    >
                        {getFieldDecorator('Phan_loai', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập phân loại phim!',
                            }],
                            initialValue: Phan_loai
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Thời lượng (Phút)"
                    >
                        {getFieldDecorator('Thoi_luong', {
                            rules: [{
                                type: 'number', message: 'Thời lượng phải là số!',
                            },
                            {
                                required: true, message: 'Vui lòng nhập thời lượng phim!',
                            }],
                            initialValue: Thoi_luong
                        })(
                            <InputNumber />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label={(
                            <span>
                                Quốc gia&nbsp;
                            <Tooltip title="Phân cách bởi dấu phẩy">
                                    <Icon type="question-circle-o" />
                                </Tooltip>
                            </span>
                        )}
                    >
                        {getFieldDecorator('Quoc_gia', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập Quốc gia phim!',
                            }],
                            initialValue: Quoc_gia
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label={(
                            <span>
                                Thể loại&nbsp;
                            <Tooltip title="Phân cách bởi dấu phẩy">
                                    <Icon type="question-circle-o" />
                                </Tooltip>
                            </span>
                        )}
                    >
                        {getFieldDecorator('The_loai', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập thể loại phim!',
                            }],
                            initialValue: The_loai
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label={(
                            <span>
                                Diễn viên&nbsp;
                            <Tooltip title="Phân cách bởi dấu phẩy">
                                    <Icon type="question-circle-o" />
                                </Tooltip>
                            </span>
                        )}
                    >
                        {getFieldDecorator('Dien_vien', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập diễn viên!',
                            }],
                            initialValue: Dien_vien
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label={(
                            <span>
                                Đạo diễn&nbsp;
                            <Tooltip title="Phân cách bởi dấu phẩy">
                                    <Icon type="question-circle-o" />
                                </Tooltip>
                            </span>
                        )}
                    >
                        {getFieldDecorator('Dao_dien', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập đạo diễn!',
                            }],
                            initialValue: Dao_dien
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Nhà sản xuất"
                    >
                        {getFieldDecorator('Nha_san_xuat', {
                            rules: [{
                                required: true, message: 'Vui lòng nhập nhà sản xuất!',
                            }],
                            initialValue: Nha_san_xuat
                        })(
                            <Input />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Ngày khởi chiếu"
                    >
                        {getFieldDecorator('Khoi_chieu', {
                            rules: [{ type: 'object', required: true, message: 'Vui lòng chọn ngày khởi chiếu!' }],
                            initialValue: Khoi_chieu ? moment(Khoi_chieu) : moment()
                        })(
                            <DatePicker format={dateFormat} />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Đơn giá (VNĐ)"
                    >
                        {getFieldDecorator('Don_gia', {
                            rules: [{
                                type: 'number', message: 'Đơn giá phải là số!',
                            },
                            {
                                required: true, message: 'Vui lòng nhập Dơn giá phim!',
                            }],
                            initialValue: Don_gia
                        })(
                            <InputNumber />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Doanh thu (VNĐ)"
                    >
                        {getFieldDecorator('Doanh_thu', {
                            rules: [{
                                type: 'number', message: 'Doanh thu phải là số!',
                            },
                            {
                                required: true, message: 'Vui lòng nhập Doanh thu phim!',
                            }],
                            // initialValue: Doanh_thu
                        })(
                            <InputNumber />
                        )}
                    </FormItem>
                    <FormItem
                        {...formItemLayout}
                        label="Nội dung Phim"
                    >
                        {getFieldDecorator('Noi_dung', {
                            rules: [
                                {
                                    required: true, message: 'Vui lòng nhập Nội dung phim!',
                                }],
                            initialValue: Noi_dung
                        })(
                            <TextArea autosize />
                        )}
                    </FormItem>

                    <h3>Suất chiếu</h3>
                    {this.props.Danh_sach_Suat_chieu.value &&
                        <SessionForm
                            movieID={this.props.Ma_so.value}
                            /// delete from state: pass func ()=> this.props.onChange
                            addmode="state"
                            // deleteSession={this.handleDeleteSession} 
                            onChange={this.props.onChange} 
                            cinemas={this.props.cinemas}
                            getFieldDecorator={getFieldDecorator}
                            getFieldValue={getFieldValue}
                            sessions={this.props.Danh_sach_Suat_chieu.value}
                            tailFormItemLayout={tailFormItemLayout} />}


                    <FormItem {...tailFormItemLayout}>
                        <Button onClick={this.showModal} >Thêm Suất chiếu</Button>
                        {/* modal form */}
                    </FormItem>
                    <FormItem {...tailFormItemLayout}>
                        <Button type="primary" htmlType="submit">{btn}</Button>
                    </FormItem>
                </Form>

                <CollectionCreateForm
                    wrappedComponentRef={this.saveFormRef}
                    visible={this.state.visible}
                    onCancel={this.handleCancel}
                    onCreate={this.handleCreate}
                    cinemas={this.props.cinemas}
                />

                {
                    this.props.error && this.onFail(this.props.error)
                }
                {
                    this.props.success && this.onSuccess(this.props.success)
                }
            </div>
        );
    }
}

const AddForm = Form.create({
    onFieldsChange(props, changedFields) {
        // console.log('field change', changedFields);
        props.onChange(changedFields);
    },
    mapPropsToFields(props) {
        // console.log('mapPropsToFields', props);
        return {
            Danh_sach_Suat_chieu: Form.createFormField({
                ...props.Danh_sach_Suat_chieu,
                value: props.Danh_sach_Suat_chieu.value,
            }), 
            Doanh_thu: Form.createFormField({
                ...props.Doanh_thu,
                value: props.Doanh_thu.value,
            }), 
            Phan_loai: Form.createFormField({
                ...props.Phan_loai,
                value: props.Phan_loai.value,
            }), 
            Don_gia: Form.createFormField({
                ...props.Don_gia,
                value: props.Don_gia.value,
            }), 
            Khoi_chieu: Form.createFormField({
                ...props.Khoi_chieu,
                value: props.Khoi_chieu.value,
            }), 
            Nha_san_xuat: Form.createFormField({
                ...props.Nha_san_xuat,
                value: props.Nha_san_xuat.value,
            }), 
            Dao_dien: Form.createFormField({
                ...props.Dao_dien,
                value: props.Dao_dien.value,
            }), 
            Dien_vien: Form.createFormField({
                ...props.Dien_vien,
                value: props.Dien_vien.value,
            }), 
            The_loai: Form.createFormField({
                ...props.The_loai,
                value: props.The_loai.value,
            }), 
            Quoc_gia: Form.createFormField({
                ...props.Quoc_gia,
                value: props.Quoc_gia.value,
            }), 
            Thoi_luong: Form.createFormField({
                ...props.Thoi_luong,
                value: props.Thoi_luong.value,
            }), 
            Rating: Form.createFormField({
                ...props.Rating,
                value: props.Rating.value,
            }), 
            Noi_dung: Form.createFormField({
                ...props.Noi_dung,
                value: props.Noi_dung.value,
            }), 
            Ten: Form.createFormField({
                ...props.Ten,
                value: props.Ten.value,
            }), 
            Ma_so: Form.createFormField({
                ...props.Ma_so,
                value: props.Ma_so.value,
            }), 
       
        };
    },
    
})(MyForm);

export default AddForm;