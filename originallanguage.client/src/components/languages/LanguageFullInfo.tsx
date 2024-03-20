import { Descriptions, Typography } from "antd";
import ILanguage from "../../models/ILanguage";
import { useParams } from "react-router-dom";
import { useLanguages } from "../../hooks/languages";
import { useEffect, useState } from "react";
import LanguageFlag from "./LanguageFlag";
import Chat from "../../chats/components/Chat";
import { ChatGroupUtils } from "../../chats/chat-group-utils";

const { Title, Paragraph } = Typography;

export default function LanguageFullInfo() {
  const { id: languageId } = useParams();
  const { getLanguage } = useLanguages();

  const [language, setLanguage] = useState<ILanguage>();

  useEffect(() => {
    if (languageId) {
      getLanguage(languageId).then((lang) => {
        console.log("language (use effect)", lang);
        setLanguage(lang);
      });
    }
  }, [languageId]);

  return !language ? (
    <div></div>
  ) : (
    <>
      <Title level={3}>
        {language.flagUrl && <LanguageFlag src={language.flagUrl} />}{" "}
        {language.name}
      </Title>
      {language.nativeName && (
        <Title level={4} style={{ color: "#858585" }}>
          {language.nativeName}
        </Title>
      )}

      {language.conlangData && (
        <>
          <Title level={4}>Conlang</Title>
          <Descriptions column={2}>
            <Descriptions.Item label="Type">
              {language.conlangData.type}
            </Descriptions.Item>
            <Descriptions.Item label="Origin">
              {language.conlangData.origin}
            </Descriptions.Item>
            <Descriptions.Item label="Articulation">
              {language.conlangData.articulation}
            </Descriptions.Item>
            <Descriptions.Item label="Subjective Complexity">
              {language.conlangData.subjectiveComplexity}
            </Descriptions.Item>
            <Descriptions.Item label="Development Status">
              {language.conlangData.developmentStatus}
            </Descriptions.Item>
            <Descriptions.Item label="Setting Origin">
              {language.conlangData.settingOrigin}
            </Descriptions.Item>
            <Descriptions.Item label="Not Humanoid Speakers">
              {language.conlangData.notHumanoidSpeakers.toString()}
            </Descriptions.Item>
            <Descriptions.Item label="Furry Speakers">
              {language.conlangData.furrySpeakers.toString()}
            </Descriptions.Item>
          </Descriptions>
        </>
      )}

      {language.about && (
        <>
          <Title level={4}>About</Title>
          <Paragraph>
            <div dangerouslySetInnerHTML={{ __html: language.about }} />
          </Paragraph>
        </>
      )}

      {language.aboutNativeSpeakers && (
        <>
          <Title level={4}>Native speakers</Title>
          <Paragraph>
            <div
              dangerouslySetInnerHTML={{ __html: language.aboutNativeSpeakers }}
            />
          </Paragraph>
        </>
      )}

      {language.links && (
        <>
          <Title level={4}>Links</Title>
          <Paragraph>
            <div dangerouslySetInnerHTML={{ __html: language.links }} />
          </Paragraph>
        </>
      )}

      <Descriptions column={2}>
        <Descriptions.Item label="Created">
          {language.dateTimeCreated.toLocaleString()}
        </Descriptions.Item>
        <Descriptions.Item label="Updated">
          {language.dateTimeUpdated.toLocaleString()}
        </Descriptions.Item>
      </Descriptions>

      <Title level={4}>{language.name} public chat</Title>
      <Chat groupId={ChatGroupUtils.getLanguageGroupId(language.id)} />
    </>
  );
}
