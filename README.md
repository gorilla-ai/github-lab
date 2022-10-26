- [dotnet-code-coverage.yml](#dotnet-code-coverageyml)
  - [目的](#目的)
  - [使用方式](#使用方式)
  - [Workflow Input](#workflow-input)
  - [Workflow 成份](#workflow-成份)
    - [XML 報告轉 HTML](#xml-報告轉-html)
    - [分析報告](#分析報告)
    - [加入 PR 評論](#加入-pr-評論)

# [dotnet-code-coverage.yml](./.github/workflows/dotnet-code-coverage.yml)

## 目的

透過 GitHub Action 
1. 將 C# 專案輸出的 Code Coverage 報告，轉為可讀性較佳的 HTML、Markdown 總結
2. HTML 報告加入 artifact - `CoverageReport`
3. 作為 PR 健康度依據

請參考[PR](https://github.com/gorilla-ai/github-lab/pull/1)。

## 使用方式

```yml
jobs:
  build:
    steps:
    # 1. 測試時指定收集 `XPlat Code Coverage`、輸出至 `TestResults`
    - name: Test
      run: dotnet test --collect:"XPlat Code Coverage" -r "TestResults"
    # 2. 上傳 `TestResults` 內檔案至名為 CoverageCobertura 的 artifact，作為 dotnet-code-coverage.yml 取得的來源
    - name: Upload Code Coverage results
      if: always()
      uses: actions/upload-artifact@v2
      with:
        name: CoverageCobertura
        path: TestResults/**/coverage.cobertura.xml

  code-coverage:
    uses: gorilla-ai/github-lab/.github/workflows/dotnet-code-coverage.yml@main
    # 3. 新增相依於測試的 job
    needs: build
    with:
      # 4. 告知儲存 `TestResults` 的 artifact 名稱
      artifactNameOfCoverageCobertura: CoverageCobertura
```

## Workflow Input

| key                             | Description                         | Default Value |
| ------------------------------- | ----------------------------------- | ------------- |
| artifactNameOfCoverageCobertura | 儲存 Code Coverage 的 artifact name |               |
| fail_below_min                  | 當覆蓋率低於最低值是否為失敗        | true          |
| thresholds                      | 覆蓋率健康指數                      | 60 80         |

## Workflow 成份
### XML 報告轉 HTML

透過第三方 GitHub Action - [ReportGenerator]，達到：
1. Cobertura XML 轉為 可讀性較佳的 HTML

### 分析報告

透過第三方 GitHub Action - [Code Coverage Summary]，達到：
1. 解析 [Coverlet], [gcovr], [simplecov], [MATLAB] 輸出的 Cobertura XML
2. 設定涵蓋率的下限值，並且設定未達下限值為失敗
3. 將 XML 轉成簡化的 Markdown 格式
4. 輸出徽章

### 加入 PR 評論

透過第三方 GitHub Action - [Sticky Pull Request Comment]，達到：
1. 將 Code Coverage 的總結加入 PR 評論：
    > ![Code Coverage](https://img.shields.io/badge/Code%20Coverage-56%25-critical?style=flat)
    > | Package     | Line Rate        | Branch Rate      | Health |
    > | ----------- | ---------------- | ---------------- | ------ |
    > | sample      | 56%              | 60%              | ❌      |
    > | **Summary** | **56%** (9 / 16) | **60%** (6 / 10) | ❌      |
    > _Minimum allowed line rate is `60%`_

[ReportGenerator]: https://github.com/danielpalme/ReportGenerator-GitHub-Action
[Code Coverage Summary]: https://github.com/marketplace/actions/code-coverage-summary
[Sticky Pull Request Comment]: https://github.com/marketplace/actions/sticky-pull-request-comment
[Coverlet]: https://github.com/coverlet-coverage/coverlet
[gcovr]: https://github.com/gcovr/gcovr
[simplecov]: https://github.com/simplecov-ruby/simplecov
[MATLAB]: https://uk.mathworks.com/help/matlab/ref/matlab.unittest.plugins.codecoverageplugin-class.html
[coverlet.collector]: https://www.nuget.org/packages/coverlet.collector