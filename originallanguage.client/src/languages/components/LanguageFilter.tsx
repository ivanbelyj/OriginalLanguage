import { Select } from "antd";
const { Option } = Select;

const conlangOptionValue = "conlang";
const notConlangOptionValue = "notConlang";

function toArtificialityOptionValue(value: boolean | null) {
  if (value === null) return "";
  return value ? conlangOptionValue : notConlangOptionValue;
}

function fromArtificialityOptionValue(value: string) {
  return value === conlangOptionValue
    ? true
    : value === notConlangOptionValue
    ? false
    : null;
}

interface ILanguageFilterProps {
  isConlang: boolean | null;
  setIsConlang: (isConlang: boolean | null) => void;
}

export const LanguageFilter: React.FC<ILanguageFilterProps> = ({
  isConlang,
  setIsConlang,
}) => {
  return (
    <Select
      value={toArtificialityOptionValue(isConlang)}
      onChange={(value) => {
        setIsConlang(fromArtificialityOptionValue(value));
      }}
      style={{ width: "100%" }}
    >
      <Option value="">Any</Option>
      <Option value={conlangOptionValue}>Conlang</Option>
      <Option value={notConlangOptionValue}>Not Conlang</Option>
    </Select>
  );
};
