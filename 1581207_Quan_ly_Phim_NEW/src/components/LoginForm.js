import React from "react";

import { Form, Icon, Input, Button, Checkbox, Alert } from 'antd';
const FormItem = Form.Item;

class NormalLoginForm extends React.Component {
    handleSubmit = (e) => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
            if (!err && values.hasOwnProperty('Ma_so') && values.hasOwnProperty('Mat_khau')) {
                console.log('Received values of form: ', values);
                this.props.requestLogin(values);
            }
        });
    }
    render() {
        const { getFieldDecorator } = this.props.form;
        return (
            <Form onSubmit={this.handleSubmit} className="login-form">
                <FormItem>
                    {getFieldDecorator('Ma_so', {
                        rules: [{ required: true, message: 'Vui lòng nhập tên đăng nhập!' }],
                    })(
                        <Input prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Tên đăng nhập" />
                    )}
                </FormItem>
                <FormItem>
                    {getFieldDecorator('Mat_khau', {
                        rules: [{ required: true, message: 'Vui lòng nhập mật khẩu!' }],
                    })(
                        <Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Mật khẩu" />
                    )}
                </FormItem>
                <FormItem>
                    <Button type="primary" htmlType="submit" className="login-form-button">
                        Đăng nhập
                    </Button>
                </FormItem>
                {
                    this.props.error && <Alert
                        message='Đăng nhập không hợp lệ'
                        description="Vui lòng thử lại."
                        type="error"
                        showIcon
                    />
                }
            </Form>
        );
    }
}

const LoginForm = Form.create()(NormalLoginForm);

export default LoginForm;