import { useEffect, useState } from "react";
import {
  Form,
  Input,
  Checkbox,
  Button,
  Typography,
  Tooltip,
  Select,
  message,
} from "antd";
import { IUpdateLanguage, useLanguages } from "../../hooks/languages";
import { useParams } from "react-router-dom";
import React from "react";

import "./edit-language.css";

const { Title } = Typography;
const { Option } = Select;
const { useWatch } = Form;

export function EditLanguage() {
  const { updateLanguage, getLanguage } = useLanguages();
  const { id: languageId } = useParams();
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();

  const languageName = useWatch("name", form);
  const [isConlang, setIsConlang] = useState(false);

  const handleFinish = async (values: React.FormEvent) => {
    console.log("Handle finish. values", values);

    if (!languageId) return;

    const langData: IUpdateLanguage = {
      ...form.getFieldsValue(),
      authorId: import.meta.env.VITE_DEBUG_USER_ID, // Todo: actual author
    };
    if (!isConlang) delete langData.conlangData;
    await updateLanguage(languageId, langData);
    messageApi.open({
      type: "success",
      content: "Language is saved",
    });
  };

  useEffect(() => {
    if (languageId) {
      getLanguage(languageId).then((language) => {
        console.log("language (use effect)", language);

        setIsConlang(language.conlangData !== undefined);
        form.setFieldsValue(language);
      });
    }
  }, [languageId]);

  return (
    <>
      {contextHolder}
      <Form form={form} onFinish={handleFinish}>
        <Title level={3}>{languageName}</Title>

        <Form.Item
          label="Language name"
          name="name"
          rules={[
            { required: true, message: "Please enter the language name" },
          ]}
        >
          <Input type="text" />
        </Form.Item>
        <Form.Item label="Language native name" name="nativeName">
          <Input type="text" />
        </Form.Item>
        <Form.Item label="About" name="about">
          <Input.TextArea placeholder="About language"></Input.TextArea>
        </Form.Item>
        <Form.Item label="About native speakers" name="aboutNativeSpeakers">
          <Input.TextArea placeholder="About native speakers"></Input.TextArea>
        </Form.Item>
        <Form.Item label="Links" name="links">
          <Input.TextArea placeholder="Links"></Input.TextArea>
        </Form.Item>

        <Form.Item label="Is constructed language">
          <Checkbox
            onChange={() => setIsConlang(!isConlang)}
            checked={isConlang}
          />
        </Form.Item>

        <div
          className={`conlang-data conlang-data_${
            isConlang ? "showing" : "hidden"
          }`}
        >
          <Form.Item>
            <Title level={4}>Constructed language</Title>
          </Form.Item>
          <Form.Item label="Type" name={["conlangData", "type"]}>
            <Select>
              <Option value="notSpecified">Not Specified</Option>
              <Option value="artistic">Artistic</Option>
              <Option value="auxiliary">Auxiliary</Option>
              <Option value="engineered">Engineered</Option>
            </Select>
          </Form.Item>

          <Form.Item label="Origin" name={["conlangData", "origin"]}>
            <Select>
              <Option value="notSpecified">Not Specified</Option>
              <Option value="apriori">Apriori</Option>
              <Option value="aposteriori">Aposteriori</Option>
            </Select>
          </Form.Item>
          <Form.Item
            label="Articulation"
            name={["conlangData", "articulation"]}
          >
            <Select>
              <Option value="common">
                <Tooltip
                  title={
                    "Select if all sounds are found " +
                    " in human languages and can be represented using IPA"
                  }
                >
                  Common
                </Tooltip>
              </Option>
              <Option value="inhumanSounds">
                <Tooltip
                  title={
                    "Select if there are any sounds that are " +
                    "not naturally produced by humans"
                  }
                >
                  Inhuman Sounds
                </Tooltip>
              </Option>
              <Option value="totallyAlien">
                <Tooltip
                  title={
                    "Select if the spoken language has a completely " +
                    "alien articulation compared to humans"
                  }
                >
                  Totally Alien
                </Tooltip>
              </Option>

              <Option value="other">
                <Tooltip
                  title={
                    "Select if the language uses some other symbolic system " +
                    "that is not related to sound producing"
                  }
                >
                  Other
                </Tooltip>
              </Option>
            </Select>
          </Form.Item>
          <Form.Item
            label="Subjective Complexity"
            name={["conlangData", "subjectiveComplexity"]}
          >
            <Select>
              <Option value="notSpecified">Not Specified</Option>
              <Option value="easy">Easy</Option>
              <Option value="moderate">Moderate</Option>
              <Option value="complicated">Complicated</Option>
            </Select>
          </Form.Item>
          <Form.Item
            label="Development Status"
            name={["conlangData", "developmentStatus"]}
          >
            <Select>
              <Option value="notSpecified">Not Specified</Option>
              <Option value="new">New</Option>
              <Option value="progressing">Progressing</Option>
              <Option value="functional">Functional</Option>
              <Option value="complete">Complete</Option>
              <Option value="onHold">On Hold</Option>
              <Option value="abandoned">Abandoned</Option>
            </Select>
          </Form.Item>

          <Form.Item>
            <Title level={4}>Native speakers</Title>
            <Form.Item
              label="Setting Origin"
              name={["conlangData", "settingOrigin"]}
            >
              <Select>
                <Option value="notSpecified">Not Specified</Option>
                <Option value="alternativeEarth">Alternative Earth</Option>
                <Option value="apriori">Apriori</Option>
              </Select>
            </Form.Item>
            <Form.Item
              label="Not Humanoid"
              name={["conlangData", "notHumanoidSpeakers"]}
              valuePropName="checked"
            >
              <Checkbox />
            </Form.Item>

            <Tooltip
              placement="bottomLeft"
              title={
                "Check if native speakers combine anthropomorphic and some " +
                "animal traits (or they can be considered as furry, if you want)"
              }
            >
              <Form.Item
                label="Furry"
                name={["conlangData", "furrySpeakers"]}
                valuePropName="checked"
              >
                <Checkbox />
              </Form.Item>
            </Tooltip>
          </Form.Item>
        </div>

        <Form.Item>
          <Button type="primary" htmlType="submit">
            Save
          </Button>
        </Form.Item>
      </Form>
    </>
  );
}
