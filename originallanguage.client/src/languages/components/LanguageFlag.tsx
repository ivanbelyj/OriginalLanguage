export interface ILanguageFlagProps {
  src?: string;
}

export default function LanguageFlag({
  src = "https://conlang.org/cms/wp-content/uploads/conlang_flag_144.png",
}: ILanguageFlagProps) {
  const sizeMultiplier = 0.8;
  return (
    <img
      src={src}
      alt="Flag"
      style={{
        width: `${3 * sizeMultiplier}em`,
        height: `${2 * sizeMultiplier}em`,
      }}
    />
  );
}
